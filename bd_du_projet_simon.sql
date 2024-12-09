-- Tables

-- adherent
CREATE TABLE adherent
(
    matricule VARCHAR(110) NOT NULL PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    adresse VARCHAR(150) NOT NULL,
    dateNaissance DATE NOT NULL,
    age INT NOT NULL
);

-- DROP TABLE adherent;


-- administrateur
CREATE TABLE administrateur (
    nom_utilisateur VARCHAR(150) PRIMARY KEY,
    mot_de_passe VARCHAR(150) NOT NULL
);

-- DROP TABLE administrateur;


-- categorie
CREATE TABLE categorie_activite
(
    idCategorie INT AUTO_INCREMENT PRIMARY KEY,
    type VARCHAR(100) NOT NULL
);

-- DROP TABLE categorie_activite;


-- activite
CREATE TABLE activite
(
    idActivite INT PRIMARY KEY AUTO_INCREMENT,
    nomActivite VARCHAR(100) NOT NULL,
    idCategorie INT NOT NULL,
    description VARCHAR(250) NOT NULL,
    cout_organisation DOUBLE NOT NULL,
    prix_vente_client DOUBLE NOT NULL,
    FOREIGN KEY (idCategorie) REFERENCES categorie_activite(idCategorie)
);

-- DROP TABLE activite;


-- seance
CREATE TABLE seance (
    idSeance INT PRIMARY KEY AUTO_INCREMENT,
    idActivite INT NOT NULL,
    date_seance DATE NOT NULL,
    heure VARCHAR(100) NOT NULL,
    nbr_place_disponible INT NOT NULL,
    nbr_inscription INT NOT NULL,
    moyenne_appreciation DOUBLE NOT NULL,
    FOREIGN KEY (idActivite) REFERENCES activite(idActivite)
);

-- DROP TABLE seance;


-- appreciation
CREATE TABLE appreciation (
    idAppreciation INT PRIMARY KEY AUTO_INCREMENT,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    note_appreciation DOUBLE NOT NULL DEFAULT 0.0,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

-- DROP TABLE appreciation;


-- reservation
CREATE TABLE reservation (
    idReservation INT PRIMARY KEY AUTO_INCREMENT,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

-- DROP TABLE reservation;



-- Triggers

-- 3.1
-- Créer le matricule pour un adhérent avant son insertion à l'aide d'une fonction stockée
DELIMITER //
CREATE TRIGGER set_matriculeAdherent
BEFORE INSERT
ON adherent
FOR EACH ROW
BEGIN
set NEW.matricule=Generer_matricule_adherent(NEW.prenom, NEW.nom, NEW.dateNaissance);
END//
DELIMITER ;

-- Créer le matricule pour un adhérent avant son update à l'aide d'une fonction stockée
DELIMITER //
CREATE TRIGGER set_matriculeAdherent_update
BEFORE UPDATE
ON adherent
FOR EACH ROW
BEGIN
set NEW.matricule=Generer_matricule_adherent(NEW.prenom, NEW.nom, NEW.dateNaissance);
END//
DELIMITER ;


-- 3.2
-- Gérer le nombre de places disponibles dans chaque séances. Ajouter un participant (adhérent)
-- augmente le nombre de places (incrémentation)
DELIMITER //
CREATE TRIGGER check_nbr_places_disponibles
BEFORE INSERT
ON reservation
FOR EACH ROW
BEGIN
    DECLARE places_disponibles INT;

    SELECT nbr_place_disponible - nbr_inscription
    INTO places_disponibles
    FROM seance
    WHERE idSeance = NEW.idSeance;

    IF places_disponibles <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT="Impossible d'ajouter la réservation: la séance est complète.";
    END IF ;
END//
DELIMITER ;

DELIMITER //
CREATE TRIGGER add_place_chq_adherent
AFTER INSERT
ON reservation
FOR EACH ROW
BEGIN
    UPDATE seance
    SET nbr_inscription = nbr_inscription + 1
    WHERE idSeance = NEW.idSeance;
END//
DELIMITER ;


-- 3.3
-- Insérer les participants (adhérents) dans une séance (réservation)
DELIMITER //
CREATE TRIGGER add_adherent_reservation
BEFORE INSERT
ON reservation
FOR EACH ROW
BEGIN
    DECLARE places_disponibles INT;

    SET places_disponibles = Places_disponibles(NEW.idSeance);

    IF places_disponibles <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT="Impossible d'ajouter la réservation: la séance est complète.";
    END IF ;
END//
DELIMITER ;

-- DROP TRIGGER add_adherent_reservation;


-- Mettre à jour la moyenne des notes d'une(des) activité(s) d'une séance
DELIMITER //
CREATE TRIGGER set_moyenne_notes_seance
AFTER INSERT
ON appreciation
FOR EACH ROW
BEGIN
    DECLARE t_moyenne_notes DOUBLE;

    SET t_moyenne_notes = Calculer_moyenne_notes_une_seance(NEW.idSeance);

    UPDATE seance
    SET moyenne_appreciation = t_moyenne_notes
    WHERE idSeance = NEW.idSeance;
END//
DELIMITER ;

-- DROP TRIGGER set_moyenne_notes_seance;


-- Mettre à jour l'âge d'un adhérent avant son insertion en utilisant une fonction stockée
DELIMITER //
CREATE TRIGGER set_age_adherent
BEFORE INSERT
ON adherent
FOR EACH ROW
BEGIN
    DECLARE age INT;
    SET age = Calculer_age_adherent(NEW.dateNaissance);

    IF age < 18 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'L\'adhérent doit avoir au moins 18 ans.';
    ELSE
        SET NEW.age = age;
    END IF;
END//
DELIMITER ;


-- Mettre à jour l'âge d'un adhérent avant son update en utilisant une fonction stockée
DELIMITER //
CREATE TRIGGER set_age_adherent_update
BEFORE UPDATE
ON adherent
FOR EACH ROW
BEGIN
    DECLARE new_age INT;

    IF OLD.dateNaissance != NEW.dateNaissance THEN
        SET new_age = Calculer_age_adherent(NEW.dateNaissance);

        IF new_age < 18 THEN
            SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'L\'adhérent doit avoir au moins 18 ans.';
        ELSE
            SET NEW.age = new_age;
        END IF;
    END IF;
END//
DELIMITER ;

DROP TRIGGER set_age_adherent_update;


-- Retirer toutes les entrées reliées à un adhérent qui a été retiré
DELIMITER //
CREATE TRIGGER delete_infos_adherent
BEFORE DELETE
ON adherent
FOR EACH ROW
BEGIN
    DELETE FROM appreciation WHERE matricule = OLD.matricule;

    DELETE FROM reservation WHERE matricule = OLD.matricule;
END//
DELIMITER ;


-- Retirer toutes les entrées reliées à une activité qui a été retirée
DELIMITER //
CREATE TRIGGER delete_infos_activite
BEFORE DELETE
ON activite
FOR EACH ROW
BEGIN
    DELETE FROM seance WHERE idActivite = OLD.idActivite;
END//
DELIMITER ;


-- Insertion des données

-- Données table adherent
INSERT INTO adherent (matricule, nom, prenom, adresse, dateNaissance, age) VALUES
('JD-1990-123', 'Doe', 'John', '123 Rue Principale', '1990-05-15', 34),
('MJ-1985-456', 'Smith', 'Marie', '456 Avenue Royale', '1985-09-10', 39),
('LP-1992-789', 'Poirier', 'Luc', '789 Boulevard Saint-Laurent', '1992-03-21', 32),
('CA-1988-112', 'Adams', 'Caroline', '112 Chemin des Pionniers', '1988-07-08', 36),
('TR-1995-223', 'Roy', 'Thomas', '223 Rue Saint-Paul', '1995-11-17', 29),
('NB-1993-334', 'Bouchard', 'Nicolas', '334 Route de l\'Église', '1993-02-25', 31),
('EC-1998-445', 'Côté', 'Élise', '445 Rue de la Montagne', '1998-12-12', 25),
('FF-1987-556', 'Fournier', 'François', '556 Allée des Pins', '1987-01-04', 37),
('MG-1994-667', 'Gagnon', 'Mélanie', '667 Rue Cartier', '1994-06-30', 30),
('BP-1990-778', 'Pelletier', 'Bernard', '778 Rue des Lilas', '1990-10-15', 34);



-- Données table administrateur
INSERT INTO administrateur (nom_utilisateur, mot_de_passe) VALUES
('admin', 'Secret1234');


-- Données table categorie_activite
INSERT INTO categorie_activite (type)
VALUES
('Sport individuel'),
('Sport d’équipe'),
('Fitness'),
('Détente'),
('Activité aquatique'),
('Sport de plein air'),
('Art martial'),
('Danse et expression corporelle'),
('Aventure'),
('Conditionnement physique');


-- Données table activite
INSERT INTO activite (idActivite, nomActivite, idCategorie, description, cout_organisation, prix_vente_client)
VALUES
(1, 'Cours de Yoga', 1, 'Séance de yoga relaxante pour améliorer la flexibilité.', 50.00, 75.00),
(2, 'Match de Football', 2, 'Partie amicale de football avec deux équipes.', 200.00, 300.00),
(3, 'Séance de Pilates', 3, 'Exercices de renforcement musculaire doux.', 60.00, 90.00),
(4, 'Atelier de Méditation', 4, 'Apprentissage des techniques de relaxation et de pleine conscience.', 40.00, 60.00),
(5, 'Cours de Natation', 5, 'Enseignement des bases de la natation pour débutants.', 100.00, 150.00),
(6, 'Randonnée Guidée', 6, 'Exploration de sentiers en pleine nature avec un guide.', 80.00, 120.00),
(7, 'Cours de Karaté', 7, 'Initiation aux techniques fondamentales du karaté.', 70.00, 100.00),
(8, 'Cours de Salsa', 8, 'Apprentissage des pas de base de la salsa en groupe.', 50.00, 80.00),
(9, 'Escalade Extérieure', 9, 'Session d’escalade sur paroi naturelle encadrée par un professionnel.', 120.00, 180.00),
(10, 'Camp d’Entraînement', 10, 'Programme intensif de conditionnement physique.', 150.00, 250.00);


-- Données table seance
INSERT INTO seance (idSeance, idActivite, date_seance, heure, nbr_place_disponible, nbr_inscription, moyenne_appreciation) VALUES
(1, 1, '2024-11-25', '18:00', 10, 0, 0.0),
(2, 2, '2024-11-26', '19:00', 15, 0, 0.0),
(3, 3, '2024-11-27', '14:00', 8, 0, 0.0),
(4, 4, '2024-11-28', '10:00', 12, 0, 0.0),
(5, 5, '2024-11-29', '16:00', 6, 0, 0.0),
(6, 6, '2024-12-01', '08:00', 20, 0, 0.0),
(7, 7, '2024-12-02', '15:00', 10, 0, 0.0),
(8, 8, '2024-12-03', '18:30', 10, 0, 0.0),
(9, 9, '2024-12-04', '17:00', 12, 0, 0.0),
(10, 10, '2024-12-05', '11:00', 5, 0, 0.0);


-- Données table appreciation
INSERT INTO appreciation (idSeance, matricule, note_appreciation) VALUES
(1, 'BP-1990-778', 4.5),
(2, 'CA-1988-112', 3.8),
(3, 'EC-1998-445', 5.0),
(4, 'FF-1987-556', 4.0),
(5, 'JD-1990-123', 2.5),
(6, 'LP-1992-789', 4.2),
(7, 'MG-1994-667', 3.9),
(8, 'MJ-1985-456', 3.5),
(9, 'NB-1993-334', 4.8),
(10, 'TR-1995-223', 4.7);


-- Données table reservation
INSERT INTO reservation (idSeance, matricule) VALUES
(1, 'BP-1990-778'),
(2, 'CA-1988-112'),
(3, 'EC-1998-445'),
(4, 'FF-1987-556'),
(5, 'JD-1990-123'),
(6, 'LP-1992-789'),
(7, 'MG-1994-667'),
(8, 'MJ-1985-456'),
(9, 'NB-1993-334'),
(10, 'TR-1995-223');



-- Vues

-- Afficher le nombre total d'adhérents
CREATE VIEW nb_total_adherent AS
SELECT COUNT(matricule) AS nb_adherents
FROM adherent;


-- Trouver le participant ayant le nombre de séances le plus élevé
CREATE VIEW nb_seances_plus_eleve AS
SELECT a.prenom,
       a.nom,
       COUNT(r.idReservation) AS nb_seances
FROM adherent a
JOIN reservation r on a.matricule = r.matricule
GROUP BY a.matricule
ORDER BY nb_seances DESC
LIMIT 1;


-- Trouver le prix moyen par activité pour chaque participant
CREATE VIEW prix_moyen_par_activite_chq_adherent AS
SELECT a.prenom,
       a.nom,
       ac.nomActivite,
       AVG(ac.prix_vente_client) AS prix_moyen
FROM reservation r
JOIN adherent a on r.matricule = a.matricule
JOIN seance s on s.idSeance = r.idSeance
JOIN activite ac on ac.idActivite = s.idActivite
GROUP BY a.matricule, ac.nomActivite;


-- Afficher les notes d’appréciation pour chaque activité
CREATE VIEW notes_appreciation_chq_activite AS
SELECT a.nomActivite,
       ad.prenom,
       ad.nom,
       ROUND(ap.note_appreciation, 1) AS note_appreciation
FROM activite a
JOIN seance s on a.idActivite = s.idActivite
JOIN appreciation ap on s.idSeance = ap.idSeance
JOIN adherent ad on ap.matricule = ad.matricule
GROUP BY a.nomActivite, note_appreciation;


-- Afficher la moyenne des notes d’appréciation pour chaque activité
CREATE VIEW moy_notes_appreciation_chq_activite AS
SELECT a.nomActivite,
       ROUND(AVG(ap.note_appreciation), 1) AS note_appreciation
FROM activite a
JOIN seance s on a.idActivite = s.idActivite
JOIN appreciation ap on s.idSeance = ap.idSeance
GROUP BY a.nomActivite;


-- Afficher le nombre de participant pour chaque activité
CREATE VIEW nb_participant_chq_activite AS
SELECT a.nomActivite,
       COUNT(DISTINCT r.matricule) AS nombre_participant
FROM reservation r
JOIN seance s on r.idSeance = s.idSeance
JOIN activite a on s.idActivite = a.idActivite
GROUP BY a.nomActivite;


-- Afficher le nombre de participant moyen pour chaque mois
CREATE VIEW nb_participant_chq_mois AS
SELECT MONTH(s.date_seance) AS mois,
       a.nomActivite,
       ROUND(AVG(nombre_participant), 0) AS nombre_participant_moyen
FROM (SELECT r.idSeance,
             COUNT(DISTINCT r.matricule) AS nombre_participant
      FROM reservation r
      JOIN seance s on s.idSeance = r.idSeance
      GROUP BY r.idSeance) AS participants_par_seance
JOIN seance s on participants_par_seance.idSeance = s.idSeance
JOIN activite a on s.idActivite = a.idActivite
GROUP BY mois, a.nomActivite;


-- Afficher les informations d'un adhérent à partir de son matricule
CREATE VIEW infos_adherent AS
SELECT matricule,
       nom,
       prenom,
       adresse,
       dateNaissance
FROM adherent;


-- Afficher le nombre d'activités total
CREATE VIEW nb_activites AS
SELECT COUNT(idActivite) AS nb_activite
FROM activite;



-- Procédures stockées

-- Ajouter un adhérent
DELIMITER //
CREATE procedure Ajouter_adherent(
    IN p_nom VARCHAR(100),
    IN p_prenom VARCHAR(100),
    IN p_adresse VARCHAR(150),
    IN p_dateNaissance DATE)
BEGIN
    INSERT INTO adherent(matricule, nom, prenom, adresse, dateNaissance, age)
    VALUES ('', p_nom, p_prenom, p_adresse, p_dateNaissance, 0);
END//
DELIMITER ;

-- Appel à la procédure
-- CALL Ajouter_adherent('Mac Donald', 'Étienne', '123 Rue Test', '2000-01-01');


-- Supprimer un adhérent
DELIMITER //
CREATE PROCEDURE Supprimer_adherent(
    IN p_matricule VARCHAR(110))
BEGIN
    IF NOT EXISTS(SELECT matricule FROM adherent WHERE matricule = p_matricule) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Ce matricule est inexistant.';
    END IF;

    DELETE FROM adherent WHERE matricule = p_matricule;
END//
DELIMITER ;

-- Appel à la procédure
-- CALL Supprimer_adherent('dshdagda');
-- CALL Supprimer_adherent('ÉM-2000-194');


-- Modifier un adhérent
DELIMITER //
CREATE PROCEDURE Modifier_adherent(
    IN p_matricule VARCHAR(110),
    IN p_nom VARCHAR(100),
    IN p_prenom VARCHAR(100),
    IN p_adresse VARCHAR(150),
    IN p_dateNaissance DATE)
BEGIN
    IF NOT EXISTS(SELECT 1 FROM adherent WHERE matricule = p_matricule) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Ce matricule est inexistant.';
    ELSE
        UPDATE adherent
        SET
            nom = p_nom,
            prenom = p_prenom,
            adresse = p_adresse,
            dateNaissance = p_dateNaissance
        WHERE matricule = p_matricule;
    END IF;
END//
DELIMITER ;

-- CALL Modifier_adherent('ÉM-2000-843', 'Cartier', 'Simon', '123 Rue Sigma', '2000-01-02');


-- Ajouter une catégorie
DELIMITER //
CREATE procedure Ajouter_categorie_activite(
    IN p_type VARCHAR(100))
BEGIN
    INSERT INTO categorie_activite(type)
    VALUES (p_type);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_categorie_activite('E-Sport');


-- Modifier une catégorie
DELIMITER //
CREATE PROCEDURE Modifier_categorie_activite(
    IN p_idCategorie INT(11),
    IN p_type VARCHAR(100))
BEGIN
    IF NOT EXISTS(SELECT 1 FROM categorie_activite WHERE idCategorie = p_idCategorie) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Cet identifiant de catégorie est inexistant.';
    ELSE
        UPDATE categorie_activite
        SET
            type = p_type
        WHERE idCategorie = p_idCategorie;
    END IF;
END//
DELIMITER ;

-- CALL Modifier_categorie_activite(33, 'E-Sportif');


-- Supprimer une catégorie
DELIMITER //
CREATE PROCEDURE Supprimer_categorie_activite(
    IN p_idCategorie VARCHAR(110))
BEGIN
    IF NOT EXISTS(SELECT idCategorie FROM categorie_activite WHERE idCategorie = p_idCategorie) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Cet identifiant de catégorie est inexistant.';
    END IF;

    IF EXISTS(SELECT idCategorie FROM activite WHERE idCategorie = p_idCategorie) THEN
        SIGNAL SQLSTATE '23000'
        SET MESSAGE_TEXT = 'Impossible de supprimer cette catégorie car elle est associée à une ou plusieurs activités.';
    END IF;

    DELETE FROM categorie_activite WHERE idCategorie = p_idCategorie;
END//
DELIMITER ;

-- Appel à la procédure
-- CALL Supprimer_categorie_activite(22);


-- Ajouter une activité
DELIMITER //
CREATE procedure Ajouter_activite(
    IN p_nomActivite VARCHAR(100),
    IN p_idCategorie INT,
    IN p_description VARCHAR(250),
    IN p_cout_organisation DOUBLE,
    IN p_prix_vente_client DOUBLE)
BEGIN
    INSERT INTO activite(nomActivite, idCategorie, description, cout_organisation, prix_vente_client)
    VALUES (p_nomActivite, p_idCategorie, p_description, p_cout_organisation, p_prix_vente_client);
END//
DELIMITER ;

-- Appel à la procédure
-- CALL Ajouter_activite('Fortnite', 11, 'The low taper fade meme is still MASSIVE', 250.0, 10.0);

-- DROP PROCEDURE Ajouter_activite;

-- Modifier une activité
DELIMITER //
CREATE PROCEDURE Modifier_activite(
    IN p_idActivite INT(11),
    IN p_nomActivite VARCHAR(100),
    IN p_idCategorie INT(11),
    IN p_description VARCHAR(250),
    IN p_cout_organisation DOUBLE,
    IN p_prix_vente_client DOUBLE)
BEGIN
    IF NOT EXISTS(SELECT 1 FROM activite WHERE idActivite = p_idActivite) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Cet identification d\'activité est inexistant.';
    ELSE
        UPDATE activite
        SET
            nomActivite = p_nomActivite,
            idCategorie = p_idCategorie,
            description = p_description,
            cout_organisation = p_cout_organisation,
            prix_vente_client = p_prix_vente_client
        WHERE idActivite = p_idActivite;
    END IF;
END//
DELIMITER ;

-- CALL Modifier_activite(11, 'FN', 11, 'The low taper fade meme is still MASSIVE, yeah no MASSIVE', 275.00, 15.00);


-- Supprimer une activité
DELIMITER //
CREATE PROCEDURE Supprimer_activite(
    IN p_idActivite INT(11))
BEGIN
    IF NOT EXISTS(SELECT idActivite FROM activite WHERE idActivite = p_idActivite) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Cet identification d\'activité est inexistant.';
    END IF;

    DELETE FROM activite WHERE idActivite = p_idActivite;
END//
DELIMITER ;

-- Appel à la procédure
-- CALL Supprimer_activite(11);


-- Ajouter une séance
DELIMITER //
CREATE procedure Ajouter_seance(
    IN p_idActivite INT,
    IN p_date_seance DATE,
    IN p_heure VARCHAR(100),
    IN p_nbr_place_disponible INT,
    IN p_nbr_inscription INT,
    IN p_moyenne_appreciation DOUBLE)
BEGIN
    INSERT INTO seance(idActivite, date_seance, heure, nbr_place_disponible, nbr_inscription, moyenne_appreciation)
    VALUES (p_idActivite, p_date_seance, p_heure, p_nbr_place_disponible, p_nbr_inscription, p_moyenne_appreciation);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_seance(10, '2024-12-05', '18:00', 10, 0, 0.0);

-- DROP PROCEDURE Ajouter_seance;


-- Modifier une séance
DELIMITER //
CREATE PROCEDURE Modifier_seance(
    IN p_idSeance INT(11),
    IN p_idActivite INT(11),
    IN p_date_seance DATE,
    IN p_heure VARCHAR(100),
    IN p_nbr_place_disponible INT(11))
BEGIN
    IF NOT EXISTS(SELECT 1 FROM seance WHERE idSeance = p_idSeance) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Cet identification de séance est inexistant.';
    ELSE
        UPDATE seance
        SET
            idActivite = p_idActivite,
            date_seance = p_date_seance,
            heure = p_heure,
            nbr_place_disponible = p_nbr_place_disponible
        WHERE idSeance = p_idSeance;
    END IF;
END//
DELIMITER ;

-- CALL Modifier_seance(10, 9, '2024-12-05', '10:00', 10);


-- Supprimer une séance
DELIMITER //
CREATE PROCEDURE Supprimer_seance(
    IN p_idSeance INT(11))
BEGIN
    IF NOT EXISTS(SELECT idSeance FROM seance WHERE idSeance = p_idSeance) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Cet identification de séance est inexistant.';
    END IF;

    DELETE FROM seance WHERE idSeance = p_idSeance;
END//
DELIMITER ;

-- Appel à la procédure
-- CALL Supprimer_seance(10);


-- Ajouter une appréciation
DELIMITER //
CREATE procedure Ajouter_appreciation(
    IN p_idSeance INT(11),
    IN p_matricule VARCHAR(110),
    IN p_note_appreciation DOUBLE)
BEGIN
    INSERT INTO appreciation(idSeance, matricule, note_appreciation)
    VALUES (p_idSeance, p_matricule, p_note_appreciation);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_appreciation(11, 'BP-1990-778', 5.0);

-- DROP PROCEDURE Ajouter_appreciation;


-- Ajouter une réservation
DELIMITER //
CREATE procedure Ajouter_reservation(
    IN p_idSeance INT(11),
    IN p_matricule VARCHAR(110))
BEGIN
    INSERT INTO reservation(idSeance, matricule)
    VALUES (p_idSeance, p_matricule);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_reservation(10, 'BP-1990-778');


-- Fonctions stockées

-- Calculer l'âge d'un adhérent
DELIMITER //
CREATE FUNCTION Calculer_age_adherent(f_dateNaissance DATE)
RETURNS INT
DETERMINISTIC
BEGIN
    DECLARE age INT;
    SET age = TIMESTAMPDIFF(YEAR, f_dateNaissance, CURDATE());

    IF MONTH(f_dateNaissance) > MONTH(CURDATE()) OR (MONTH(f_dateNaissance) = MONTH(CURDATE()) AND DAY(f_dateNaissance) > DAY(CURDATE())) THEN
        SET age = age - 1;
    END IF;

    RETURN age;
END //
DELIMITER ;


-- Générer le matricule d'un adhérent
DELIMITER //
CREATE FUNCTION Generer_matricule_adherent(f_prenom VARCHAR(100), f_nom VARCHAR(100), f_dateNaissance DATE)
RETURNS VARCHAR(110)
DETERMINISTIC
BEGIN
    RETURN CONCAT(UPPER(LEFT(f_prenom, 1)), UPPER(LEFT(f_nom, 1)), '-', YEAR(f_dateNaissance), '-', LPAD(FLOOR(100 + RAND() * 900), 3, '0'));
END //
DELIMITER ;


-- Calculer la moyenne des notes d'une séance
DELIMITER //
CREATE FUNCTION Calculer_moyenne_notes_une_seance(f_idSeance INT(11))
RETURNS DOUBLE
DETERMINISTIC
BEGIN
    DECLARE moyenne DOUBLE;

    SELECT ROUND(AVG(note_appreciation), 1) INTO moyenne
    FROM appreciation
    WHERE idSeance = f_idSeance;

    RETURN moyenne;
END //
DELIMITER ;

-- DROP FUNCTION Calculer_moyenne_notes_une_seance;


DELIMITER //
CREATE FUNCTION Places_disponibles(f_idSeance INT(11))
RETURNS INT
DETERMINISTIC
BEGIN
    DECLARE places_disponibles INT;

    SELECT nbr_place_disponible - nbr_inscription
    INTO places_disponibles
    FROM seance
    WHERE idSeance = f_idSeance;

    RETURN places_disponibles;
END //
DELIMITER ;

-- DROP FUNCTION Places_disponibles;



-- MODIFICATIONS À VÉRIFIER