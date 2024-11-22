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