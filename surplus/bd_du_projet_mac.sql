-- Étienne Mac Donald, Simon cartier
-- projet final de programmation graphique et de base de données


-- shéma rationnel
/*
adherent(pk matricule, nom, prenom, adresse, dateNaissance, age)
reservation(pk idReservation, #idSeance, # matricule)
appreciation(pk idApprecitation, note_appreciation, # matricule, # idSeance)
seance(pk idSeance, date_seance, heure, nbr_place_disponible, nbr_inscription, moyenne_appreciation, # idActivite)
activite(pk idActivite, nomActivite, description, cout_organisation, prix_vente_client, # idCategorie)
categorie_activite(pk idCategorie, type)

administrateur(pk nom_utilisateur, mot_de_passe)
*/

-- creation des tables
CREATE TABLE adherent(
    matricule VARCHAR(110) PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    adresse VARCHAR(150) NOT NULL,
    dateNaissance DATE NOT NULL,
    age INT NOT NULL
);

CREATE TABLE activite(
    idActivite INT PRIMARY KEY,
    nom_activite VARCHAR(100) NOT NULL,
    type VARCHAR(100) NOT NULL,
    description VARCHAR(250) NOT NULL,
    cout_organisation DOUBLE NOT NULL,
    prix_vente_client DOUBLE NOT NULL
);

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

CREATE TABLE appreciation (
    idAppreciation INT PRIMARY KEY,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    note_appreciation DOUBLE NOT NULL,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

CREATE TABLE reservation (
    idReservation INT PRIMARY KEY AUTO_INCREMENT,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

CREATE TABLE administrateur (
    nom_utilisateur VARCHAR(150) PRIMARY KEY,
    mot_de_passe VARCHAR(150) NOT NULL
);

-- création des données
-- Données pour la table `adherent`
INSERT INTO adherent (matricule, nom, prenom, adresse, dateNaissance, age) VALUES
('AD001', 'Dupont', 'Jean', '123 Rue de Paris, Paris', '1985-04-15', 39),
('AD002', 'Martin', 'Claire', '45 Avenue des Lilas, Lyon', '1990-10-12', 34),
('AD003', 'Durand', 'Paul', '78 Boulevard Haussmann, Marseille', '1995-07-22', 29),
('AD004', 'Bernard', 'Lucie', '22 Rue des Fleurs, Lille', '2000-03-08', 24),
('AD005', 'Petit', 'Lucas', '90 Rue des Peupliers, Nantes', '1988-11-03', 36);

-- Données pour la table `activite`
INSERT INTO activite (idActivite, nom_activite, type, description, cout_organisation, prix_vente_client) VALUES
(1, 'Yoga', 'Sport', 'Cours de yoga pour tous niveaux.', 150.0, 200.0),
(2, 'Peinture', 'Art', 'Ateliers de peinture à lhuile.', 100.0, 150.0),
(3, 'Randonnée', 'Nature', 'Excursion en montagne.', 300.0, 400.0),
(4, 'Cuisine', 'Culinaire', 'Cours de cuisine gastronomique.', 200.0, 250.0),
(5, 'Théâtre', 'Culture', 'Initiation au théâtre.', 120.0, 180.0);

-- Données pour la table `seance`
INSERT INTO seance (idSeance, idActivite, date_seance, heure, nbr_place_disponible, nbr_inscription, moyenne_appreciation) VALUES
(1, 1, '2024-01-15', '10:00', 20, 15, 4.5),
(2, 2, '2024-01-20', '14:00', 15, 12, 4.2),
(3, 3, '2024-02-10', '08:00', 30, 25, 4.8),
(4, 4, '2024-02-15', '18:00', 10, 8, 4.0),
(5, 5, '2024-03-05', '16:00', 25, 20, 4.6);

-- Données pour la table `appreciation`
INSERT INTO appreciation (idAppreciation, idSeance, matricule, note_appreciation) VALUES
(1, 1, 'AD001', 5.0),
(2, 1, 'AD002', 4.0),
(3, 2, 'AD003', 4.5),
(4, 3, 'AD004', 4.8),
(5, 4, 'AD005', 4.3);

-- Données pour la table `reservation`
INSERT INTO reservation (idReservation, idSeance, matricule) VALUES
(1, 1, 'AD001'),
(2, 2, 'AD002'),
(3, 3, 'AD003'),
(4, 4, 'AD004'),
(5, 5, 'AD005');

-- Données pour la table `administrateur`
INSERT INTO administrateur (nom_utilisateur, mot_de_passe) VALUES
('admin1', 'password123'),
('admin2', 'securePass2024'),
('admin3', 'admin@321'),
('admin4', 'mypassword'),
('admin5', 'pass!word');



-- création des views
CREATE VIEW participant_au_plus_de_seance AS
SELECT
    nom,
    prenom,
    COUNT(a.idSeance) AS nbr_de_seance
FROM adherent
INNER JOIN appreciation a on adherent.matricule = a.matricule
INNER JOIN seance s on a.idSeance = s.idSeance
GROUP BY a.matricule
ORDER BY nbr_de_seance DESC
LIMIT 1;
/////////////////////////////////////////////////////////////
-- creation des tables
CREATE TABLE adherent(
    matricule VARCHAR(110) PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    adresse VARCHAR(150) NOT NULL,
    dateNaissance DATE NOT NULL
);

CREATE TABLE activite(
     idActivite INT PRIMARY KEY,
     nomActivite VARCHAR(100) NOT NULL,
     type VARCHAR(100) NOT NULL,
     description VARCHAR(250) NOT NULL,
     cout_organisation DOUBLE NOT NULL,
     prix_vente_client DOUBLE NOT NULL
);

CREATE TABLE seance (
     idSeance INT PRIMARY KEY,
     idActivite INT NOT NULL,
     dateSeance DATE NOT NULL,
     heure VARCHAR(100) NOT NULL,
     nbr_place_disponible INT NOT NULL,
     FOREIGN KEY (idActivite) REFERENCES activite(idActivite)
);

CREATE TABLE appreciation (
    idAppreciation INT PRIMARY KEY,
    idSeance INT NOT NULL,
    matricule VARCHAR(110) NOT NULL,
    note_appreciation INT NOT NULL,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (matricule) REFERENCES adherent(matricule)
);

CREATE TABLE administrateur (
    nom_utilisateur VARCHAR(150) PRIMARY KEY,
    mot_de_passe VARCHAR(150) NOT NULL
);
INSERT INTO adherent (matricule, nom, prenom, adresse, dateNaissance) VALUES
('A001', 'Dupont', 'Pierre', '123 Rue de Paris, 75001 Paris', '1985-01-15'),
('A002', 'Martin', 'Sophie', '45 Avenue de la République, 75011 Paris', '1990-07-22'),
('A003', 'Lemoine', 'Julien', '67 Boulevard Saint-Germain, 75005 Paris', '1982-10-30'),
('A004', 'Tremblay', 'Claire', '89 Rue des Fleurs, 75012 Paris', '1995-03-11'),
('A005', 'Lemoine', 'Marc', '23 Rue de la Liberté, 75013 Paris', '1987-11-02');

INSERT INTO activite (idActivite, nomActivite, type, description, cout_organisation, prix_vente_client) VALUES
(1, 'Yoga', 'Bien-être', 'Séance de yoga pour la relaxation et le renforcement musculaire.', 50.0, 20.0),
(2, 'Natation', 'Sport', 'Cours de natation pour tous les niveaux.', 70.0, 30.0),
(3, 'Peinture', 'Art', 'Atelier de peinture acrylique pour débutants et confirmés.', 40.0, 25.0),
(4, 'Danse', 'Loisir', 'Cours de danse contemporaine et classique.', 60.0, 35.0),
(5, 'Course à pied', 'Sport', 'Séances dentrainement pour la course à pied en extérieur.', 30.0, 15.0);

INSERT INTO seance (idSeance, idActivite, dateSeance, heure, nbr_place_disponible) VALUES
(1, 1, '2024-12-01', '09:00', 15),
(2, 2, '2024-12-02', '10:30', 20),
(3, 3, '2024-12-03', '14:00', 12),
(4, 4, '2024-12-04', '16:00', 10),
(5, 5, '2024-12-05', '18:00', 25);

INSERT INTO appreciation (idAppreciation, idSeance, matricule, note_appreciation) VALUES
(1, 1, 'A001', 4),
(2, 2, 'A002', 5),
(3, 3, 'A003', 3),
(4, 4, 'A004', 5),
(5, 5, 'A005', 4);

-- création des views
-- adherent plus de seance
CREATE VIEW adherent_au_plus_de_seance AS
SELECT
    nom,
    prenom,
    COUNT(a.idSeance) AS nbr_de_seance
FROM adherent
INNER JOIN appreciation a on adherent.matricule = a.matricule
INNER JOIN seance s on a.idSeance = s.idSeance
GROUP BY a.matricule
ORDER BY nbr_de_seance DESC
LIMIT 1;

-- prix moyen par adherent
-- CREATE VIEW prix_moyen_par_adherent AS
SELECT
    nom,
    prenom,
    SUM(prix_vente_client)/(

        ) AS somme_prix_activite
FROM adherent
INNER JOIN appreciation a on adherent.matricule = a.matricule
INNER JOIN seance s on a.idSeance = s.idSeance
INNER JOIN activite a2 on s.idActivite = a2.idActivite
GROUP BY a.matricule;
-- SELECT nbr_seance_par_adherent('A001');

DELIMITER //
CREATE fonction nbr_seance_par_adherent(_matricule VARCHAR(11))
RETURNS INT
BEGIN
    DECLARE nbrsenance INT;
    SET idClient = CONCAT(
            _num_csv,
            '-',
            MONTH(_dateNaissance)
                       );
    RETURN(idCLient);
end//
Delimiter ;


SELECT
    a.matricule,
    nom,
    prenom,
    COUNT(s.idActivite) AS nbr_activite
FROM adherent
INNER JOIN appreciation a on adherent.matricule = a.matricule
INNER JOIN seance s on a.idSeance = s.idSeance
INNER JOIN activite a2 on s.idActivite = a2.idActivite
GROUP BY a.matricule;














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