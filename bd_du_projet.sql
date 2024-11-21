-- Étienne Mac Donald, Simon cartier
-- projet final de programmation graphique et de base de données


-- shéma rationnel
/*
adherent(pk idAdherent, nom, prenom, dateNaissance, adresse)
activite(pk idActivite, nomActivite, type, description, coutOrganisation, prixVenteClient)
seance(pk idSeance, date, heure, nbr fk idAdherent, fk idActivite)
appreciation(pk idApprecitation, fk idAdherent, fk idActivite)
*/


-- creation des tables
CREATE TABLE adherent(
    idAdherent INT PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    adresse VARCHAR(150) NOT NULL,
    dateNaissance DATE NOT NULL
);

CREATE TABLE activite(
     idActivite INT PRIMARY KEY,
     nom VARCHAR(100) NOT NULL,
     type VARCHAR(100) NOT NULL,
     description VARCHAR(250) NOT NULL,
     coutOrganisation DOUBLE NOT NULL,
     prixVente DOUBLE NOT NULL
);

CREATE TABLE seance (
     idSeance INT PRIMARY KEY,
     idActivite INT NOT NULL,
     dateSeance DATE NOT NULL,
     heure VARCHAR(100) NOT NULL,
     nbrPlaceDisponible INT NOT NULL,
     FOREIGN KEY (idActivite) REFERENCES activite(idActivite)
);

CREATE TABLE appreceation (
    idAppreciation INT PRIMARY KEY,
    idSeance INT NOT NULL,
    idAdherent INT NOT NULL,
    noteAppreciation INT NOT NULL,
    FOREIGN KEY (idSeance) REFERENCES seance(idSeance),
    FOREIGN KEY (idAdherent) REFERENCES adherent(idAdherent)
);