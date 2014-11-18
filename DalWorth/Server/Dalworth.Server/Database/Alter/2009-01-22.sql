TRUNCATE EquipmentTransactionDetailLink;

INSERT INTO EquipmentTransactionDetailLink (EquipmentTransactionDetailId)
	SELECT ID FROM EquipmentTransactionDetail;

UPDATE EquipmentTransactionDetailLink etdl
	INNER JOIN EquipmentTransactionDetail etd on etdl.EquipmentTransactionDetailId = etd.ID
	SET PrevEquipmentTransactionDetailId = FindPrevEquipmentTransactionDetailId(etd.EquipmentId, etd.ID);