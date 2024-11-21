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