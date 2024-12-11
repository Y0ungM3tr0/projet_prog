/************** PROJET BD PAR SIMON CARTIER ET ÉTIENNE MAC DONALD **************/

/************** TABLES **************/


-- Table administrateur
CREATE TABLE administrateur (
    nom_utilisateur VARCHAR(150) PRIMARY KEY,
    mot_de_passe VARCHAR(150) NOT NULL
);

-- Table categorie_activite
CREATE TABLE categorie_activite
(
    idCategorie INT AUTO_INCREMENT PRIMARY KEY,
    type VARCHAR(100) NOT NULL
);

-- Table activite
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

-- Table seance
CREATE TABLE seance (
    idSeance INT PRIMARY KEY AUTO_INCREMENT,
    idActivite INT NOT NULL,
    date_seance DATE NOT NULL,
    heure VARCHAR(100) NOT NULL,
    nbr_place_disponible INT NOT NULL,
    nbr_inscription INT DEFAULT 0 NOT NULL,
    moyenne_appreciation DOUBLE DEFAULT 0.0 NOT NULL,
    FOREIGN KEY (idActivite) REFERENCES activite(idActivite)
);

-- Table adherent
CREATE TABLE adherent
(
    matricule VARCHAR(110) NOT NULL PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    adresse VARCHAR(150) NOT NULL,
    dateNaissance DATE NOT NULL,
    age INT NOT NULL
);

-- Table appreciation
CREATE TABLE appreciation (
    idAppreciation INT PRIMARY KEY AUTO_INCREMENT,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    note_appreciation DOUBLE NOT NULL DEFAULT 0.0,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

-- Table reservation
CREATE TABLE reservation (
    idReservation INT PRIMARY KEY AUTO_INCREMENT,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

-- Cascades
-- Table appreciation
ALTER TABLE appreciation
MODIFY matricule VARCHAR(110),
ADD CONSTRAINT FOREIGN KEY (matricule) REFERENCES adherent (matricule) ON UPDATE CASCADE;

ALTER TABLE appreciation
DROP FOREIGN KEY appreciation_ibfk_2;

-- Table reservation
ALTER TABLE reservation
MODIFY matricule VARCHAR(110),
ADD CONSTRAINT FOREIGN KEY (matricule) REFERENCES adherent (matricule) ON UPDATE CASCADE;

ALTER TABLE reservation
DROP FOREIGN KEY reservation_ibfk_2;

-- Table seance
ALTER TABLE seance
MODIFY idActivite INT,
ADD CONSTRAINT FOREIGN KEY (idActivite) REFERENCES activite (idActivite) ON UPDATE CASCADE;

ALTER TABLE seance
DROP FOREIGN KEY seance_ibfk_1;


/************** FIN TABLES **************/



/************** TRIGGERS **************/


-- Table administrateur
-- Aucun trigger pour la table administrateur


-- Table activite
-- 1. Retirer toutes les entrées reliées à une activité qui a été retirée
DELIMITER //
CREATE TRIGGER delete_infos_activite
BEFORE DELETE
ON activite
FOR EACH ROW
BEGIN
    DELETE FROM seance WHERE idActivite = OLD.idActivite;
END//
DELIMITER ;


-- Table seance
-- 1. Empêcher l'inscription d'un adhérent à une séance s'il est déjà inscrit
DELIMITER //
CREATE TRIGGER prevent_ajout_sur_seance
BEFORE INSERT
ON reservation
FOR EACH ROW
BEGIN
    IF EXISTS(
        SELECT 1
        FROM reservation
        WHERE idSeance = NEW.idSeance
        AND matricule = NEW.matricule
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = "Cet adhérent est déja inscrit à cette séance.";
    END IF;
END//
DELIMITER ;

-- 2. Retirer toutes les entrées reliées à une séance qui a été retirée
DELIMITER //
CREATE TRIGGER delete_infos_seance
BEFORE DELETE
ON seance
FOR EACH ROW
BEGIN
    DELETE FROM appreciation WHERE idSeance = OLD.idSeance;

    DELETE FROM reservation WHERE idSeance = OLD.idSeance;
END//
DELIMITER ;


-- Table categorie_activite
-- Aucun trigger pour la table categorie_activite


-- Table adherent
-- 3.1 Créer le matricule pour un adhérent avant son insertion à l'aide d'une fonction stockée
DELIMITER //
CREATE TRIGGER set_matricule_adherent
BEFORE INSERT
ON adherent
FOR EACH ROW
BEGIN
    set NEW.matricule=Generer_matricule_adherent(NEW.prenom, NEW.nom, NEW.dateNaissance);
END//
DELIMITER ;

-- Créer le matricule pour un adhérent avant sa modification à l'aide d'une fonction stockée
DELIMITER //
CREATE TRIGGER set_matricule_adherent_update
BEFORE UPDATE
ON adherent
FOR EACH ROW
BEGIN
set NEW.matricule=Generer_matricule_adherent(NEW.prenom, NEW.nom, NEW.dateNaissance);
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
        SET MESSAGE_TEXT = 'L adhérent doit avoir au moins 18 ans.';
    ELSE
        SET NEW.age = age;
    END IF;
END//
DELIMITER ;

-- Mettre à jour l'âge d'un adhérent avant sa modification en utilisant une fonction stockée
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
            SET MESSAGE_TEXT = 'L adhérent doit avoir au moins 18 ans.';
        ELSE
            SET NEW.age = new_age;
        END IF;
    END IF;
END//
DELIMITER ;

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

-- Table reservation
-- 3.2 Gérer le nombre de places disponibles pour chaque séances. Ajouter un participant
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

-- Augmenter le nombre de places (incrémentation)
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

-- 3.3 Insérer les participants dans une séance (reservation)
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


-- Table appreciation
-- Mettre à jour la moyenne des notes d'une séance après l'insertion d'une nouvelle note
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

-- Empêcher l'ajout d'une note si l'adhérent a déjà noté la séance
DELIMITER //
CREATE TRIGGER prevent_ajout_note_sur_seance
BEFORE INSERT
ON appreciation
FOR EACH ROW
BEGIN
    IF EXISTS(
        SELECT 1
        FROM appreciation
        WHERE idSeance = NEW.idSeance
        AND matricule = NEW.matricule
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Cet adhérent a déja noté cette séance.';
    END IF;
END//
DELIMITER ;


/************** FIN TRIGGERS **************/



/************** PROCÉDURES STOCKÉES **************/


-- Table administrateur
-- Aucune procédure stockée pour la table administrateur


-- Table activite
-- Ajouter une activité à la table activite
DELIMITER //
CREATE PROCEDURE Ajouter_activite(
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
        SET MESSAGE_TEXT = 'Cet identification d activité est inexistant.';
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

-- Supprimer une activité
DELIMITER //
CREATE PROCEDURE Supprimer_activite(
    IN p_idActivite INT(11))
BEGIN
    IF NOT EXISTS(SELECT idActivite FROM activite WHERE idActivite = p_idActivite) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Cet identification d activité est inexistant.';
    END IF;

    IF EXISTS(SELECT idActivite FROM seance WHERE idActivite = p_idActivite) THEN
        SIGNAL SQLSTATE '01000'
        SET MESSAGE_TEXT = 'En supprimant cette activité, la ou les séances ayant cette activité seront également supprimées.';
    END IF;

    DELETE FROM activite WHERE idActivite = p_idActivite;
END//
DELIMITER ;


-- Table seance
-- Ajouter une seance
DELIMITER //
CREATE PROCEDURE Ajouter_seance(
    IN p_idActivite INT,
    IN p_date_seance DATE,
    IN p_heure VARCHAR(100),
    IN p_nbr_place_disponible INT)
BEGIN
    INSERT INTO seance(idActivite, date_seance, heure, nbr_place_disponible, nbr_inscription, moyenne_appreciation)
    VALUES (p_idActivite, p_date_seance, p_heure, p_nbr_place_disponible, 0, 0);
END//
DELIMITER ;

-- Modifier une seance
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

-- Supprimer une seance
DELIMITER //
CREATE PROCEDURE Supprimer_seance(
    IN p_idSeance INT)
BEGIN
    IF NOT EXISTS(SELECT idSeance FROM seance WHERE idSeance = p_idSeance) THEN
        SIGNAL SQLSTATE '02000'
            SET MESSAGE_TEXT = 'Cet identification de séance est inexistant.';
    END IF;

    DELETE FROM seance WHERE idSeance = p_idSeance;
END//
DELIMITER ;


-- Table categorie_activite
-- Ajouter une categorie
DELIMITER //
CREATE procedure Ajouter_categorie_activite(
    IN p_type VARCHAR(100))
BEGIN
    INSERT INTO categorie_activite(type)
    VALUES (p_type);
END//
DELIMITER ;

-- Modifier une categorie
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

-- Supprimer une categorie (pas inclut dans le programme)
DELIMITER //
CREATE PROCEDURE Supprimer_categorie_activite(
    IN p_idCategorie VARCHAR(110))
BEGIN
    IF NOT EXISTS(SELECT idCategorie FROM categorie_activite WHERE idCategorie = p_idCategorie) THEN
        SIGNAL SQLSTATE '02000'
        SET MESSAGE_TEXT = 'Cet identifiant de catégorie est inexistant.';
    END IF;

    IF EXISTS(SELECT idCategorie FROM activite WHERE idCategorie = p_idCategorie) THEN
        SIGNAL SQLSTATE '01000'
        SET MESSAGE_TEXT = 'En supprimant cette catégorie, la ou les activités ayant cette catégorie seront également supprimées.';
    END IF;

    DELETE FROM categorie_activite WHERE idCategorie = p_idCategorie;
END//
DELIMITER ;


-- Table adherent
-- Ajouter un adherent
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

-- Modifier un adherent
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

-- Supprimer un adherent
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


-- Table reservation
-- Afficher les reservations d'un adherent
DELIMITER //
CREATE procedure infos_reservations_de_utilisateur(
    IN p_matricule VARCHAR(110))
BEGIN
    SELECT
        nomActivite,
        date_seance,
        heure
    FROM reservation
             INNER JOIN seance s on reservation.idSeance = s.idSeance
             INNER JOIN activite a on s.idActivite = a.idActivite
    WHERE matricule = p_matricule;
END//
DELIMITER ;

-- Afficher les reservations de l'utilisateur pour appréciation
DELIMITER //
CREATE procedure infos_seance_reservations_utilisateur_pour_appreciation(
    IN p_matricule VARCHAR(110))
BEGIN
    SELECT
        s.idSeance,
        a.idActivite,
        date_seance,
        heure,
        nbr_place_disponible,
        nbr_inscription,
        moyenne_appreciation,
        nomActivite,
        description,
        idReservation,
        matricule
    FROM reservation
    INNER JOIN seance s on reservation.idSeance = s.idSeance
    INNER JOIN activite a on s.idActivite = a.idActivite
    WHERE matricule = p_matricule;
END//
DELIMITER ;

-- Ajouter une reservation
DELIMITER //
CREATE procedure Ajouter_reservation(
    IN p_idSeance INT(11),
    IN p_matricule VARCHAR(110))
BEGIN
    INSERT INTO reservation(idSeance, matricule)
    VALUES (p_idSeance, p_matricule);
END//
DELIMITER ;


-- Table appreciation
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


/************** FIN PROCÉDURES STOCKÉES **************/



/************** FONCTIONS STOCKÉES **************/


-- Table administrateur
-- Aucune fonction stockée pour la table administrateur


-- Table activite
-- Aucune fonction stockée pour la table activite


-- Table seance
-- Calculer la moyenne des appréciations d'une seance
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

-- Gérer le nombre de places disponibles
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


-- Table categorie_activite
-- Aucune fonction stockée pour la table categorie_activite


-- Table adherent
-- Générer le matricule d'un adherent
DELIMITER //
CREATE FUNCTION Generer_matricule_adherent(f_prenom VARCHAR(100), f_nom VARCHAR(100), f_dateNaissance DATE)
RETURNS VARCHAR(110)
DETERMINISTIC
BEGIN
    RETURN CONCAT(UPPER(LEFT(f_prenom, 1)), UPPER(LEFT(f_nom, 1)), '-', YEAR(f_dateNaissance), '-', LPAD(FLOOR(100 + RAND() * 900), 3, '0'));
END //
DELIMITER ;

-- Calculer l'âge d'un adherent
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


-- Table appreciation
-- Aucune fonction stockée pour la table appreciation


-- Table reservation
-- Aucune fonction stockée pour la table reservation


/************** FIN FONCTIONS STOCKÉES **************/



/************** VUES **************/


-- Table administrateur
-- Aucune vue pour la table administrateur


-- Table activite
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

-- Afficher les informations d'une activité avec sa catégorie (au lieu de l'id de la catégorie)
CREATE VIEW infos_activite AS
SELECT a.idActivite,
       a.nomActivite,
       ca.type,
       a.description,
       a.cout_organisation,
       a.prix_vente_client
FROM activite a
INNER JOIN categorie_activite ca on a.idCategorie = ca.idCategorie;

-- Afficher le nombre d'activités total
CREATE VIEW nb_activites AS
SELECT COUNT(idActivite) AS nb_activite
FROM activite;

-- CREATE VIEW nbr_seance_par_activite AS
CREATE VIEW nbr_seance_par_activite AS
SELECT
    nomActivite,
    COUNT(idSeance) AS nbr_seance_par_activite
FROM activite
INNER JOIN seance s on activite.idActivite = s.idActivite
GROUP BY s.idActivite;


-- Table seance
-- Afficher les informations d'une séance avec son activité (au lieu de l'id de l'activite)
CREATE VIEW infos_seance AS
SELECT s.idSeance,
       s.idActivite,
       a.nomActivite,
       s.date_seance,
       s.heure,
       s.nbr_place_disponible,
       s.nbr_inscription,
       s.moyenne_appreciation
FROM seance s
INNER JOIN activite a on s.idActivite = a.idActivite;


-- Table categorie_activite
-- Aucune vue pour la table categorie_activite


-- Table adherent
-- Afficher le nombre total d'adhérents
CREATE VIEW nb_total_adherent AS
SELECT COUNT(matricule) AS nb_adherents
FROM adherent;

-- Trouver le participant ayant le nombre de séances le plus élevé
CREATE VIEW nb_seances_plus_eleve AS
SELECT a.matricule,
       a.prenom,
       a.nom,
       COUNT(r.idReservation) AS nb_seances
FROM adherent a
         JOIN reservation r on a.matricule = r.matricule
GROUP BY a.matricule
ORDER BY nb_seances DESC
LIMIT 1;

-- Afficher les informations d'un adhérent à partir de son matricule
CREATE VIEW infos_adherent AS
SELECT matricule,
       nom,
       prenom,
       adresse,
       dateNaissance
FROM adherent;

-- Trouver le prix moyen par activité pour chaque participant
CREATE VIEW prix_moyen_par_activite_chq_adherent AS
SELECT
    a.matricule,
    a.prenom,
    a.nom,
    ac.nomActivite,
    AVG(ac.prix_vente_client) AS moy_prix_activite
FROM reservation r
         JOIN adherent a on r.matricule = a.matricule
         JOIN seance s on s.idSeance = r.idSeance
         JOIN activite ac on ac.idActivite = s.idActivite
GROUP BY a.matricule, ac.nomActivite;


-- Table appreciation
-- Afficher la moyenne des notes (attribué aux séances) par activités
CREATE VIEW moy_note_par_activite AS
SELECT
    nomActivite,
    AVG(note_appreciation) AS moy_note_par_activite
FROM appreciation
INNER JOIN seance s on appreciation.idSeance = s.idSeance
INNER JOIN activite a on s.idActivite = a.idActivite
GROUP BY nomActivite;

-- Afficher les notes d’appréciation pour chaque activité
SELECT
    nomActivite,
    AVG(note_appreciation) AS moy_note
FROM appreciation
INNER JOIN seance s on appreciation.idSeance = s.idSeance
INNER JOIN activite a on s.idActivite = a.idActivite
GROUP BY a.idActivite;

-- Affiche la moyenne des notes d’appréciations pour toutes les activités
CREATE VIEW moy_note_toutes_activites AS
SELECT
    AVG(note_appreciation) AS moy_note_toutes_activites
FROM appreciation
INNER JOIN seance s on appreciation.idSeance = s.idSeance
INNER JOIN activite a on s.idActivite = a.idActivite;


-- Table reservation
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

-- Afficher les informations d'une réservation avec sa séance et son adhérent (prénom et nom) (au lieu de l'id de la séance et le matricule de l'adhérent)
CREATE VIEW infos_reservation AS
SELECT r.idReservation,
       r.idSeance,
       s.idActivite,
       a.nomActivite,
       r.matricule,
       a2.prenom,
       a2.nom
FROM reservation r
INNER JOIN seance s on r.idSeance = s.idSeance
INNER JOIN activite a on s.idActivite = a.idActivite
INNER JOIN adherent a2 on r.matricule = a2.matricule;


/************** FIN VUES **************/



/************** INSERTION DES DONNÉES **************/


-- Table adherent
INSERT INTO adherent (matricule, nom, prenom, adresse, dateNaissance, age) VALUES
('', 'Doe', 'John', '123 Rue Principale', '1990-05-15', 0),
('', 'Smith', 'Marie', '456 Avenue Royale', '1985-09-10', 0),
('', 'Poirier', 'Luc', '789 Boulevard Saint-Laurent', '1992-03-21', 0),
('', 'Adams', 'Caroline', '112 Chemin des Pionniers', '1988-07-08', 0),
('', 'Roy', 'Thomas', '223 Rue Saint-Paul', '1995-11-17', 0),
('', 'Bouchard', 'Nicolas', '334 Route de l Église', '1993-02-25', 0),
('', 'Côté', 'Élise', '445 Rue de la Montagne', '1998-12-12', 0),
('', 'Fournier', 'François', '556 Allée des Pins', '1987-01-04', 0),
('', 'Gagnon', 'Mélanie', '667 Rue Cartier', '1994-06-30', 0),
('', 'Pelletier', 'Bernard', '778 Rue des Lilas', '1990-10-15', 0);


-- Table administrateur
INSERT INTO administrateur (nom_utilisateur, mot_de_passe) VALUES
('admin', 'Secret1234');


-- Table categorie_activite
INSERT INTO categorie_activite (type) VALUES
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


-- Table activite
INSERT INTO activite (idActivite, nomActivite, idCategorie, description, cout_organisation, prix_vente_client) VALUES
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


-- Table seance
INSERT INTO seance (idSeance, idActivite, date_seance, heure, nbr_place_disponible) VALUES
(1, 1, '2024-11-25', '18:00', 10),
(2, 2, '2024-11-26', '19:00', 15),
(3, 3, '2024-11-27', '14:00', 8),
(4, 4, '2024-11-28', '10:00', 12),
(5, 5, '2024-11-29', '16:00', 6),
(6, 6, '2024-12-01', '08:00', 20),
(7, 7, '2024-12-02', '15:00', 10),
(8, 8, '2024-12-03', '18:30', 10),
(9, 9, '2024-12-04', '17:00', 12),
(10, 10, '2024-12-05', '11:00', 5);


-- Table appreciation
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


-- Table reservation
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


/************** FIN INSERTION DES DONNÉES **************/