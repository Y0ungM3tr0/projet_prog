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

-- activite
CREATE TABLE activite
(
    idActivite INT NOT NULL PRIMARY KEY,
    nomActivite VARCHAR(100) NOT NULL,
    type VARCHAR(100) NOT NULL,
    description VARCHAR(250) NOT NULL,
    cout_organisation double NOT NULL,
    prix_vente_client double NOT NULL
);

-- seance
CREATE TABLE seance (
    idSeance INT PRIMARY KEY,
    idActivite INT NOT NULL,
    date_seance DATE NOT NULL,
    heure VARCHAR(100) NOT NULL,
    nbr_place_disponible INT NOT NULL,
    nbr_inscription INT NOT NULL,
    moyenne_appreciation DOUBLE NOT NULL,
    FOREIGN KEY (idActivite) REFERENCES activite(idActivite)
);

-- appreciation
CREATE TABLE appreciation (
    idAppreciation INT PRIMARY KEY,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    note_appreciation DOUBLE NOT NULL DEFAULT 0.0,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

-- reservation
CREATE TABLE reservation (
    idReservation INT PRIMARY KEY AUTO_INCREMENT,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

-- administrateur
CREATE TABLE administrateur (
    nom_utilisateur VARCHAR(150) PRIMARY KEY,
    mot_de_passe VARCHAR(150) NOT NULL
);
 


-- Triggers

-- 3.1
-- Générer un matricule pour un adhérent avant son insertion
DELIMITER //
CREATE TRIGGER set_matriculeAdherent
BEFORE INSERT
ON adherent
FOR EACH ROW
BEGIN
set NEW.matricule=CONCAT(SUBSTRING(NEW.prenom, 1, 1), SUBSTRING(NEW.nom, 1, 1), '-',YEAR(NEW.datenaissance), '-', LPAD(FLOOR(RAND() * 1000), 3, '0'));
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

    SELECT nbr_place_disponible - nbr_inscription
    INTO places_disponibles
    FROM seance
    WHERE idSeance = NEW.idSeance;

    IF places_disponibles <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT="Impossible d'ajouter la réservation: la séance est complète.";
    ELSE
        INSERT INTO reservation VALUES (NULL, idSeance, matricule);
    END IF ;
END//
DELIMITER ;


-- Calculer la moyenne des notes d'une(des) activité(s) d'une séance
DELIMITER //
CREATE TRIGGER set_moyenne_notes_seance
AFTER INSERT
ON appreciation
FOR EACH ROW
BEGIN
    DECLARE t_moyenne_notes DOUBLE;

    SELECT
        ROUND(AVG(a.note_appreciation), 1) INTO t_moyenne_notes
    FROM appreciation a
    WHERE idSeance = NEW.idSeance;

    UPDATE seance
    SET moyenne_appreciation = t_moyenne_notes
    WHERE idSeance = NEW.idSeance;
END//
DELIMITER ;



-- Insertion des données
-- Données table adherent
INSERT INTO adherent (matricule, nom, prenom, adresse, dateNaissance, age) VALUES
(NULL, 'Dupont', 'Jean', '123 Rue Principale', '1985-03-12', 39),
(NULL, 'Martin', 'Claire', '456 Avenue des Fleurs', '1990-07-15', 34),
(NULL, 'Durand', 'Luc', '789 Boulevard Saint-Michel', '2000-01-25', 24),
(NULL, 'Petit', 'Emma', '12 Place de la République', '1995-11-03', 29),
(NULL, 'Morel', 'Noah', '67 Rue de Paris', '1988-06-20', 36),
(NULL, 'Lemoine', 'Sophie', '89 Chemin Vert', '1992-04-18', 32),
(NULL, 'Blanc', 'Hugo', '34 Rue des Champs', '1998-09-22', 26),
(NULL, 'Marchand', 'Alice', '11 Quai de Seine', '1997-05-30', 27),
(NULL, 'Garnier', 'Thomas', '90 Rue de Lyon', '1987-08-11', 37),
(NULL, 'Roux', 'Camille', '78 Allée des Pins', '1993-12-02', 31);

-- Données table activite
INSERT INTO activite (idActivite, nomActivite, type, description, cout_organisation, prix_vente_client) VALUES
(1, 'Yoga', 'Sport', 'Séance de yoga pour débutants', 100.0, 20.0),
(2, 'Danse', 'Sport', 'Cours de danse moderne', 150.0, 25.0),
(3, 'Cuisine', 'Loisir', 'Atelier de cuisine italienne', 200.0, 50.0),
(4, 'Peinture', 'Art', 'Cours de peinture acrylique', 80.0, 15.0),
(5, 'Musique', 'Art', 'Cours de guitare pour débutants', 120.0, 30.0),
(6, 'Randonnée', 'Sport', 'Sortie en montagne', 50.0, 10.0),
(7, 'Photographie', 'Loisir', 'Atelier de photographie en extérieur', 90.0, 25.0),
(8, 'Théâtre', 'Art', 'Atelier d\'improvisation théâtrale' ' , 70.0, 20.0),  -- <-- supprimer le "'" après "théâtrale" AVANT INSERTION !!!!!!
(9, 'Fitness', 'Sport', 'Séance de fitness intensif', 110.0, 15.0),
(10, 'Lecture', 'Loisir', 'Cercle de lecture et discussion', 30.0, 5.0);

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

-- Données table reservation
INSERT INTO reservation (idSeance, matricule) VALUES
(1, 'AM-1997-061'),
(1, 'CM-1990-851'),
(2, 'CR-1993-784'),
(2, 'EP-1995-797'),
(3, 'HB-1998-903'),
(4, 'JD-1985-329'),
(5, 'LD-2000-270'),
(6, 'NM-1988-175'),
(7, 'SL-1992-486'),
(8, 'TG-1987-593');

-- Données table appreciation
INSERT INTO appreciation (idAppreciation, idSeance, matricule, note_appreciation) VALUES
(1, 1, 'AM-1997-061', 4.5),
(2, 1, 'CM-1990-851', 4.0),
(3, 2, 'CR-1993-784', 5.0),
(4, 2, 'EP-1995-797', 4.8),
(5, 3, 'HB-1998-903', 3.5),
(6, 4, 'JD-1985-329', 4.2),
(7, 5, 'LD-2000-270', 4.0),
(8, 6, 'NM-1988-175', 4.7),
(9, 7, 'SL-1992-486', 3.8),
(10, 8, 'TG-1987-593', 5.0);

-- Données table administrateur
INSERT INTO administrateur (nom_utilisateur, mot_de_passe) VALUES
('admin', 'Secret1234');



-- Vues

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


-- Procédures stockées
-- Ajouter un adhérent
DELIMITER //
CREATE procedure Ajouter_adherent(
    IN p_nom VARCHAR(100),
    IN p_prenom VARCHAR(100),
    IN p_adresse VARCHAR(150),
    IN p_dateNaissance DATE,
    IN p_age INT)
BEGIN
    INSERT INTO adherent(matricule, nom, prenom, adresse, dateNaissance, age)
    VALUES ('', p_nom, p_prenom, p_adresse, p_dateNaissance, p_age);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_adherent('Mac Donald', 'Étienne', '123 Rue Test', '2000-01-01', 24);


-- Ajouter une activité
DELIMITER //
CREATE procedure Ajouter_activite(
    IN p_idActivite INT(11),
    IN p_nomActivite VARCHAR(100),
    IN p_type VARCHAR(100),
    IN p_description VARCHAR(250),
    IN p_cout_organisation DOUBLE,
    IN p_prix_vente_client DOUBLE)
BEGIN
    INSERT INTO activite(idActivite, nomActivite, type, description, cout_organisation, prix_vente_client)
    VALUES (p_idActivite, p_nomActivite, p_type, p_description, p_cout_organisation, p_prix_vente_client);
END//
DELIMITER ;

-- Appel à la procédure
CALL Ajouter_activite(11, 'Fortnite', 'E-Sport', 'The low taper fade meme is still MASSIVE', 250.0, 10.0);


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
CALL Ajouter_appreciation(11, 11, 'ÉM-2000-666', 5.0);


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
CALL Ajouter_reservation(11, 'ÉM-2000-666');