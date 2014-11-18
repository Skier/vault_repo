DROP TABLE ll_invoiceitemattachment;
DROP TABLE ll_file;

CREATE TABLE ll_file
(
  fileid integer NOT NULL DEFAULT nextval('ll_file_sqc'::regclass),
  origfilename text NOT NULL,
  storagekey text NOT NULL,
  userid integer NOT NULL,
  note text,
  CONSTRAINT pk_ll_file PRIMARY KEY (fileid),
  CONSTRAINT fk_ll_file_ll_user FOREIGN KEY (userid)
      REFERENCES ll_user (userid) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);



CREATE TABLE ll_invoiceitemattachment
(
  invoiceitemattachmentid integer NOT NULL DEFAULT nextval('ll_invoiceitemattachment_sqc'::regclass),
  invoiceitemid integer NOT NULL,
  fileid integer NOT NULL,
  CONSTRAINT pk_ll_invoiceitemattachment PRIMARY KEY (invoiceitemattachmentid),
  CONSTRAINT fk_ll_invoiceitemattachment_ll_file FOREIGN KEY (fileid)
      REFERENCES ll_file (fileid) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT fk_ll_invoiceitemattachment_ll_invoiceitem FOREIGN KEY (invoiceitemid)
      REFERENCES ll_invoiceitem (invoiceitemid) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
)
