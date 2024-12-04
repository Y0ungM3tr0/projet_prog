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
CREATE TRIGGER check_nbr_places_disponibles
BEFORE INSERT
ON reservation
FOR EACH ROW
BEGIN
    DECLARE places_disponibles INT;

    SET places_disponibles = Places_disponibles(NEW.idSeance);

    IF places_disponibles <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT="Impossible d'ajouter la réservation: la séance est complète.";
    ELSE
        INSERT INTO reservation VALUES (NULL, idSeance, matricule);
    END IF ;
END//
DELIMITER ;

-- DROP TRIGGER check_nbr_places_disponibles;


-- Mettre à jour la moyenne des notes d'une(des) activité(s) d'une séance
DELIMITER //
CREATE TRIGGER set_moyenne_notes_seance
AFTER INSERT
ON appreciation
FOR EACH ROW
BEGIN
    DECLARE t_moyenne_notes DOUBLE;

    SELECT
        Calculer_moyenne_notes_une_seance(note_appreciation) INTO t_moyenne_notes
    FROM appreciation a
    WHERE idSeance = NEW.idSeance;

    UPDATE seance
    SET moyenne_appreciation = t_moyenne_notes
    WHERE idSeance = NEW.idSeance;
END//
DELIMITER ;


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
('', 'Dupont', 'Jean', '123 Rue Principale', '1985-03-12', 39),
('', 'Martin', 'Claire', '456 Avenue des Fleurs', '1990-07-15', 34),
('', 'Durand', 'Luc', '789 Boulevard Saint-Michel', '2000-01-25', 24),
('', 'Petit', 'Emma', '12 Place de la République', '1995-11-03', 29),
('', 'Morel', 'Noah', '67 Rue de Paris', '1988-06-20', 36),
('', 'Lemoine', 'Sophie', '89 Chemin Vert', '1992-04-18', 32),
('', 'Blanc', 'Hugo', '34 Rue des Champs', '1998-09-22', 26),
('', 'Marchand', 'Alice', '11 Quai de Seine', '1997-05-30', 27),
('', 'Garnier', 'Thomas', '90 Rue de Lyon', '1987-08-11', 37),
('', 'Roux', 'Camille', '78 Allée des Pins', '1993-12-02', 31);


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
INSERT INTO appreciation (idAppreciation, idSeance, matricule, note_appreciation) VALUES
(1, 1, 'AM-1997-906', 4.5),
(2, 1, 'CM-1990-704', 4.0),
(3, 2, 'CR-1993-819', 5.0),
(4, 2, 'EP-1995-503', 4.8),
(5, 3, 'HB-1998-155', 3.5),
(6, 4, 'JD-1985-659', 4.2),
(7, 5, 'LD-2000-543', 4.0),
(8, 6, 'NM-1988-786', 4.7),
(9, 7, 'SL-1992-523', 3.8),
(10, 8, 'TG-1987-367', 5.0);


-- Données table reservation
INSERT INTO reservation (idSeance, matricule) VALUES
(1, 'AM-1997-906'),
(1, 'CM-1990-704'),
(2, 'CR-1993-819'),
(2, 'EP-1995-503'),
(3, 'HB-1998-155'),
(4, 'JD-1985-659'),
(5, 'LD-2000-543'),
(6, 'NM-1988-786'),
(7, 'SL-1992-523'),
(8, 'TG-1987-367');



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
CALL Ajouter_adherent('Mac Donald', 'Étienne', '123 Rue Test', '2000-01-01');


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
CALL Supprimer_adherent('dshdagda');
CALL Supprimer_adherent('ÉM-2000-194');


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

CALL Modifier_adherent('ÉM-2000-843', 'Cartier', 'Simon', '123 Rue Sigma', '2000-01-02');


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


-- Ajouter une activité
DELIMITER //
CREATE procedure Ajouter_activite(
    IN p_idActivite INT(11),
    IN p_nomActivite VARCHAR(100),
    IN p_idCategorie INT(11),
    IN p_description VARCHAR(250),
    IN p_cout_organisation DOUBLE,
    IN p_prix_vente_client DOUBLE)
BEGIN
    INSERT INTO activite(idActivite, nomActivite, idCategorie, description, cout_organisation, prix_vente_client)
    VALUES (p_idActivite, p_nomActivite, p_idCategorie, p_description, p_cout_organisation, p_prix_vente_client);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_activite(11, 'Fortnite', 11, 'The low taper fade meme is still MASSIVE', 250.0, 10.0);


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

CALL Modifier_activite(11, 'FN', 11, 'The low taper fade meme is still MASSIVE, yeah no MASSIVE', 275.00, 15.00);


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
CALL Supprimer_activite(11);


-- Ajouter une séance
DELIMITER //
CREATE procedure Ajouter_seance(
    IN p_idSeance INT(11),
    IN p_idActivite INT(11),
    IN p_date_seance DATE,
    IN p_heure VARCHAR(100),
    IN p_nbr_place_disponible INT(11),
    IN p_nbr_inscription INT(11),
    IN p_moyenne_appreciation DOUBLE)
BEGIN
    INSERT INTO seance(idSeance, idActivite, date_seance, heure, nbr_place_disponible, nbr_inscription, moyenne_appreciation)
    VALUES (p_idSeance, p_idActivite, p_date_seance, p_heure, p_nbr_place_disponible, p_nbr_inscription, p_moyenne_appreciation);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_seance(11, 11, '2024-12-05', '18:00', 10, 0, 0);


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

CALL Modifier_seance(10, 9, '2024-12-05', '10:00', 10);


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
CALL Supprimer_seance(10);


-- Ajouter une séance
DELIMITER //
CREATE procedure Ajouter_appreciation(
    IN p_idAppreciation INT(11),
    IN p_idSeance INT(11),
    IN p_matricule VARCHAR(110),
    IN p_note_appreciation DOUBLE)
BEGIN
    INSERT INTO appreciation(idAppreciation, idSeance, matricule, note_appreciation)
    VALUES (p_idAppreciation, p_idSeance, p_matricule, p_note_appreciation);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_appreciation(11, 11, 'ÉM-2000-194', 5.0);


-- Ajouter une séance
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
CALL Ajouter_reservation(11, 'ÉM-2000-194');



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
CREATE FUNCTION Calculer_moyenne_notes_une_seance(f_note_appreciation DOUBLE)
RETURNS DOUBLE
DETERMINISTIC
BEGIN
    RETURN ROUND(AVG(f_note_appreciation), 1);
END //
DELIMITER ;


DELIMITER //
CREATE FUNCTION Places_disponibles(idSeance INT)
RETURNS INT
DETERMINISTIC
BEGIN
    DECLARE places_disponibles INT;

    SELECT nbr_place_disponible - nbr_inscription
    INTO places_disponibles
    FROM seance
    WHERE idSeance = idSeance;

    RETURN places_disponibles;
END //
DELIMITER ;





-- MODIFICATIONS À VÉRIFIER