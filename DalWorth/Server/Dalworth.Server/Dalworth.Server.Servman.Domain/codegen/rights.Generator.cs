
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Servman.Domain
      {


      public partial class rights
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into rights ( " +
      
        " userid, " +
      
        " user, " +
      
        " login_name, " +
      
        " password, " +
      
        " department, " +
      
        " active, " +
      
        " online, " +
      
        " ro, " +
      
        " roc, " +
      
        " rom, " +
      
        " ron, " +
      
        " ror, " +
      
        " row, " +
      
        " rou, " +
      
        " roua, " +
      
        " rouk, " +
      
        " roud, " +
      
        " rouh, " +
      
        " rouj, " +
      
        " roul, " +
      
        " rour, " +
      
        " rous, " +
      
        " rouc, " +
      
        " roum, " +
      
        " rout, " +
      
        " roux, " +
      
        " rop, " +
      
        " ropa, " +
      
        " ropf, " +
      
        " ropd, " +
      
        " ropc, " +
      
        " rops, " +
      
        " ropn, " +
      
        " rd, " +
      
        " rde, " +
      
        " rdq, " +
      
        " rdr, " +
      
        " rds, " +
      
        " rdu, " +
      
        " rdua, " +
      
        " rduc, " +
      
        " rdud, " +
      
        " rdup, " +
      
        " rdus, " +
      
        " rdp, " +
      
        " rdpc, " +
      
        " rdpd, " +
      
        " rdpi, " +
      
        " rdpl, " +
      
        " rdpo, " +
      
        " rdpp, " +
      
        " rdps, " +
      
        " rdpb, " +
      
        " rdpn, " +
      
        " rdpq, " +
      
        " rdpu, " +
      
        " rdpa, " +
      
        " rdpaa, " +
      
        " rdp1, " +
      
        " rdj, " +
      
        " ra, " +
      
        " rac, " +
      
        " rap, " +
      
        " rapr, " +
      
        " rapa, " +
      
        " rapt, " +
      
        " rapp, " +
      
        " rapf, " +
      
        " rapc, " +
      
        " rapcb, " +
      
        " rapcbt, " +
      
        " rapcbe, " +
      
        " rapcbo, " +
      
        " rapcba, " +
      
        " rapcbp, " +
      
        " rapcbg, " +
      
        " rapcbj, " +
      
        " rapcbr, " +
      
        " rapcbrp, " +
      
        " rapcp, " +
      
        " rapcps, " +
      
        " rapcpc, " +
      
        " rapcpr, " +
      
        " rapcprp, " +
      
        " rau, " +
      
        " raua, " +
      
        " rauc, " +
      
        " raue, " +
      
        " rauh, " +
      
        " raus, " +
      
        " raum, " +
      
        " rar, " +
      
        " rar1, " +
      
        " rar2, " +
      
        " rara, " +
      
        " rars, " +
      
        " rard, " +
      
        " rarh, " +
      
        " rarl, " +
      
        " rarm, " +
      
        " rarn, " +
      
        " raru, " +
      
        " rarg, " +
      
        " rarc, " +
      
        " rarr, " +
      
        " rarb, " +
      
        " rart, " +
      
        " rari, " +
      
        " rarz, " +
      
        " rark, " +
      
        " rarw, " +
      
        " raro, " +
      
        " rarp, " +
      
        " ru, " +
      
        " rui, " +
      
        " ruo, " +
      
        " rue, " +
      
        " ruq, " +
      
        " ruw, " +
      
        " ruc, " +
      
        " rum, " +
      
        " rus, " +
      
        " rm, " +
      
        " rmr, " +
      
        " rmra, " +
      
        " rmrs, " +
      
        " rmrd, " +
      
        " rmrm, " +
      
        " rmrn, " +
      
        " rmrr, " +
      
        " rmrk, " +
      
        " rmrt, " +
      
        " rmrj, " +
      
        " rmrw, " +
      
        " rmrv, " +
      
        " rmre, " +
      
        " rmrp, " +
      
        " rmro, " +
      
        " rmrl, " +
      
        " rmry, " +
      
        " rmrc, " +
      
        " rmrf, " +
      
        " rmg, " +
      
        " rmp, " +
      
        " rs, " +
      
        " rss, " +
      
        " rssa, " +
      
        " rsse, " +
      
        " rssr, " +
      
        " rssu, " +
      
        " rssz, " +
      
        " rse, " +
      
        " rsu, " +
      
        " rsc, " +
      
        " rsi, " +
      
        " rso, " +
      
        " rsl, " +
      
        " rsv, " +
      
        " rsd, " +
      
        " rsea, " +
      
        " rseb, " +
      
        " rsec, " +
      
        " rsed, " +
      
        " rsee, " +
      
        " rsef, " +
      
        " rseg, " +
      
        " rseh, " +
      
        " rsei, " +
      
        " rsem, " +
      
        " rsen, " +
      
        " rseo, " +
      
        " rsep, " +
      
        " rser, " +
      
        " rses, " +
      
        " rset, " +
      
        " rseu, " +
      
        " rsev, " +
      
        " rsex, " +
      
        " rsey, " +
      
        " rsel, " +
      
        " rsez, " +
      
        " rcell, " +
      
        " rsysmenu, " +
      
        " sysadmin, " +
      
        " old_id, " +
      
        " savertime, " +
      
        " localprt, " +
      
        " rdc, " +
      
        " ropr, " +
      
        " rare, " +
      
        " rse1, " +
      
        " compress, " +
      
        " rmru, " +
      
        " rmrud, " +
      
        " rmrus, " +
      
        " rul, " +
      
        " rmrb, " +
      
        " rmrbt, " +
      
        " rmrbc, " +
      
        " rmrbd, " +
      
        " rmrbcl, " +
      
        " rsh, " +
      
        " pradmin, " +
      
        " unlimited, " +
      
        " rse2, " +
      
        " rse3, " +
      
        " rse4, " +
      
        " rse5, " +
      
        " rmr1, " +
      
        " rmi, " +
      
        " rmim, " +
      
        " rmir, " +
      
        " rmis, " +
      
        " rmird, " +
      
        " rmirs, " +
      
        " ropl, " +
      
        " ropt, " +
      
        " rmr2, " +
      
        " rmr3, " +
      
        " rmrba, " +
      
        " roph, " +
      
        " rmr4, " +
      
        " rse6, " +
      
        " company_id, " +
      
        " dealer " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(rights rights)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@userid", rights.userid);
      
        Database.PutParameter(dbCommand,"@user", rights.user);
      
        Database.PutParameter(dbCommand,"@login_name", rights.login_name);
      
        Database.PutParameter(dbCommand,"@password", rights.password);
      
        Database.PutParameter(dbCommand,"@department", rights.department);
      
        Database.PutParameter(dbCommand,"@active", rights.active);
      
        Database.PutParameter(dbCommand,"@online", rights.online);
      
        Database.PutParameter(dbCommand,"@ro", rights.ro);
      
        Database.PutParameter(dbCommand,"@roc", rights.roc);
      
        Database.PutParameter(dbCommand,"@rom", rights.rom);
      
        Database.PutParameter(dbCommand,"@ron", rights.ron);
      
        Database.PutParameter(dbCommand,"@ror", rights.ror);
      
        Database.PutParameter(dbCommand,"@row", rights.row);
      
        Database.PutParameter(dbCommand,"@rou", rights.rou);
      
        Database.PutParameter(dbCommand,"@roua", rights.roua);
      
        Database.PutParameter(dbCommand,"@rouk", rights.rouk);
      
        Database.PutParameter(dbCommand,"@roud", rights.roud);
      
        Database.PutParameter(dbCommand,"@rouh", rights.rouh);
      
        Database.PutParameter(dbCommand,"@rouj", rights.rouj);
      
        Database.PutParameter(dbCommand,"@roul", rights.roul);
      
        Database.PutParameter(dbCommand,"@rour", rights.rour);
      
        Database.PutParameter(dbCommand,"@rous", rights.rous);
      
        Database.PutParameter(dbCommand,"@rouc", rights.rouc);
      
        Database.PutParameter(dbCommand,"@roum", rights.roum);
      
        Database.PutParameter(dbCommand,"@rout", rights.rout);
      
        Database.PutParameter(dbCommand,"@roux", rights.roux);
      
        Database.PutParameter(dbCommand,"@rop", rights.rop);
      
        Database.PutParameter(dbCommand,"@ropa", rights.ropa);
      
        Database.PutParameter(dbCommand,"@ropf", rights.ropf);
      
        Database.PutParameter(dbCommand,"@ropd", rights.ropd);
      
        Database.PutParameter(dbCommand,"@ropc", rights.ropc);
      
        Database.PutParameter(dbCommand,"@rops", rights.rops);
      
        Database.PutParameter(dbCommand,"@ropn", rights.ropn);
      
        Database.PutParameter(dbCommand,"@rd", rights.rd);
      
        Database.PutParameter(dbCommand,"@rde", rights.rde);
      
        Database.PutParameter(dbCommand,"@rdq", rights.rdq);
      
        Database.PutParameter(dbCommand,"@rdr", rights.rdr);
      
        Database.PutParameter(dbCommand,"@rds", rights.rds);
      
        Database.PutParameter(dbCommand,"@rdu", rights.rdu);
      
        Database.PutParameter(dbCommand,"@rdua", rights.rdua);
      
        Database.PutParameter(dbCommand,"@rduc", rights.rduc);
      
        Database.PutParameter(dbCommand,"@rdud", rights.rdud);
      
        Database.PutParameter(dbCommand,"@rdup", rights.rdup);
      
        Database.PutParameter(dbCommand,"@rdus", rights.rdus);
      
        Database.PutParameter(dbCommand,"@rdp", rights.rdp);
      
        Database.PutParameter(dbCommand,"@rdpc", rights.rdpc);
      
        Database.PutParameter(dbCommand,"@rdpd", rights.rdpd);
      
        Database.PutParameter(dbCommand,"@rdpi", rights.rdpi);
      
        Database.PutParameter(dbCommand,"@rdpl", rights.rdpl);
      
        Database.PutParameter(dbCommand,"@rdpo", rights.rdpo);
      
        Database.PutParameter(dbCommand,"@rdpp", rights.rdpp);
      
        Database.PutParameter(dbCommand,"@rdps", rights.rdps);
      
        Database.PutParameter(dbCommand,"@rdpb", rights.rdpb);
      
        Database.PutParameter(dbCommand,"@rdpn", rights.rdpn);
      
        Database.PutParameter(dbCommand,"@rdpq", rights.rdpq);
      
        Database.PutParameter(dbCommand,"@rdpu", rights.rdpu);
      
        Database.PutParameter(dbCommand,"@rdpa", rights.rdpa);
      
        Database.PutParameter(dbCommand,"@rdpaa", rights.rdpaa);
      
        Database.PutParameter(dbCommand,"@rdp1", rights.rdp1);
      
        Database.PutParameter(dbCommand,"@rdj", rights.rdj);
      
        Database.PutParameter(dbCommand,"@ra", rights.ra);
      
        Database.PutParameter(dbCommand,"@rac", rights.rac);
      
        Database.PutParameter(dbCommand,"@rap", rights.rap);
      
        Database.PutParameter(dbCommand,"@rapr", rights.rapr);
      
        Database.PutParameter(dbCommand,"@rapa", rights.rapa);
      
        Database.PutParameter(dbCommand,"@rapt", rights.rapt);
      
        Database.PutParameter(dbCommand,"@rapp", rights.rapp);
      
        Database.PutParameter(dbCommand,"@rapf", rights.rapf);
      
        Database.PutParameter(dbCommand,"@rapc", rights.rapc);
      
        Database.PutParameter(dbCommand,"@rapcb", rights.rapcb);
      
        Database.PutParameter(dbCommand,"@rapcbt", rights.rapcbt);
      
        Database.PutParameter(dbCommand,"@rapcbe", rights.rapcbe);
      
        Database.PutParameter(dbCommand,"@rapcbo", rights.rapcbo);
      
        Database.PutParameter(dbCommand,"@rapcba", rights.rapcba);
      
        Database.PutParameter(dbCommand,"@rapcbp", rights.rapcbp);
      
        Database.PutParameter(dbCommand,"@rapcbg", rights.rapcbg);
      
        Database.PutParameter(dbCommand,"@rapcbj", rights.rapcbj);
      
        Database.PutParameter(dbCommand,"@rapcbr", rights.rapcbr);
      
        Database.PutParameter(dbCommand,"@rapcbrp", rights.rapcbrp);
      
        Database.PutParameter(dbCommand,"@rapcp", rights.rapcp);
      
        Database.PutParameter(dbCommand,"@rapcps", rights.rapcps);
      
        Database.PutParameter(dbCommand,"@rapcpc", rights.rapcpc);
      
        Database.PutParameter(dbCommand,"@rapcpr", rights.rapcpr);
      
        Database.PutParameter(dbCommand,"@rapcprp", rights.rapcprp);
      
        Database.PutParameter(dbCommand,"@rau", rights.rau);
      
        Database.PutParameter(dbCommand,"@raua", rights.raua);
      
        Database.PutParameter(dbCommand,"@rauc", rights.rauc);
      
        Database.PutParameter(dbCommand,"@raue", rights.raue);
      
        Database.PutParameter(dbCommand,"@rauh", rights.rauh);
      
        Database.PutParameter(dbCommand,"@raus", rights.raus);
      
        Database.PutParameter(dbCommand,"@raum", rights.raum);
      
        Database.PutParameter(dbCommand,"@rar", rights.rar);
      
        Database.PutParameter(dbCommand,"@rar1", rights.rar1);
      
        Database.PutParameter(dbCommand,"@rar2", rights.rar2);
      
        Database.PutParameter(dbCommand,"@rara", rights.rara);
      
        Database.PutParameter(dbCommand,"@rars", rights.rars);
      
        Database.PutParameter(dbCommand,"@rard", rights.rard);
      
        Database.PutParameter(dbCommand,"@rarh", rights.rarh);
      
        Database.PutParameter(dbCommand,"@rarl", rights.rarl);
      
        Database.PutParameter(dbCommand,"@rarm", rights.rarm);
      
        Database.PutParameter(dbCommand,"@rarn", rights.rarn);
      
        Database.PutParameter(dbCommand,"@raru", rights.raru);
      
        Database.PutParameter(dbCommand,"@rarg", rights.rarg);
      
        Database.PutParameter(dbCommand,"@rarc", rights.rarc);
      
        Database.PutParameter(dbCommand,"@rarr", rights.rarr);
      
        Database.PutParameter(dbCommand,"@rarb", rights.rarb);
      
        Database.PutParameter(dbCommand,"@rart", rights.rart);
      
        Database.PutParameter(dbCommand,"@rari", rights.rari);
      
        Database.PutParameter(dbCommand,"@rarz", rights.rarz);
      
        Database.PutParameter(dbCommand,"@rark", rights.rark);
      
        Database.PutParameter(dbCommand,"@rarw", rights.rarw);
      
        Database.PutParameter(dbCommand,"@raro", rights.raro);
      
        Database.PutParameter(dbCommand,"@rarp", rights.rarp);
      
        Database.PutParameter(dbCommand,"@ru", rights.ru);
      
        Database.PutParameter(dbCommand,"@rui", rights.rui);
      
        Database.PutParameter(dbCommand,"@ruo", rights.ruo);
      
        Database.PutParameter(dbCommand,"@rue", rights.rue);
      
        Database.PutParameter(dbCommand,"@ruq", rights.ruq);
      
        Database.PutParameter(dbCommand,"@ruw", rights.ruw);
      
        Database.PutParameter(dbCommand,"@ruc", rights.ruc);
      
        Database.PutParameter(dbCommand,"@rum", rights.rum);
      
        Database.PutParameter(dbCommand,"@rus", rights.rus);
      
        Database.PutParameter(dbCommand,"@rm", rights.rm);
      
        Database.PutParameter(dbCommand,"@rmr", rights.rmr);
      
        Database.PutParameter(dbCommand,"@rmra", rights.rmra);
      
        Database.PutParameter(dbCommand,"@rmrs", rights.rmrs);
      
        Database.PutParameter(dbCommand,"@rmrd", rights.rmrd);
      
        Database.PutParameter(dbCommand,"@rmrm", rights.rmrm);
      
        Database.PutParameter(dbCommand,"@rmrn", rights.rmrn);
      
        Database.PutParameter(dbCommand,"@rmrr", rights.rmrr);
      
        Database.PutParameter(dbCommand,"@rmrk", rights.rmrk);
      
        Database.PutParameter(dbCommand,"@rmrt", rights.rmrt);
      
        Database.PutParameter(dbCommand,"@rmrj", rights.rmrj);
      
        Database.PutParameter(dbCommand,"@rmrw", rights.rmrw);
      
        Database.PutParameter(dbCommand,"@rmrv", rights.rmrv);
      
        Database.PutParameter(dbCommand,"@rmre", rights.rmre);
      
        Database.PutParameter(dbCommand,"@rmrp", rights.rmrp);
      
        Database.PutParameter(dbCommand,"@rmro", rights.rmro);
      
        Database.PutParameter(dbCommand,"@rmrl", rights.rmrl);
      
        Database.PutParameter(dbCommand,"@rmry", rights.rmry);
      
        Database.PutParameter(dbCommand,"@rmrc", rights.rmrc);
      
        Database.PutParameter(dbCommand,"@rmrf", rights.rmrf);
      
        Database.PutParameter(dbCommand,"@rmg", rights.rmg);
      
        Database.PutParameter(dbCommand,"@rmp", rights.rmp);
      
        Database.PutParameter(dbCommand,"@rs", rights.rs);
      
        Database.PutParameter(dbCommand,"@rss", rights.rss);
      
        Database.PutParameter(dbCommand,"@rssa", rights.rssa);
      
        Database.PutParameter(dbCommand,"@rsse", rights.rsse);
      
        Database.PutParameter(dbCommand,"@rssr", rights.rssr);
      
        Database.PutParameter(dbCommand,"@rssu", rights.rssu);
      
        Database.PutParameter(dbCommand,"@rssz", rights.rssz);
      
        Database.PutParameter(dbCommand,"@rse", rights.rse);
      
        Database.PutParameter(dbCommand,"@rsu", rights.rsu);
      
        Database.PutParameter(dbCommand,"@rsc", rights.rsc);
      
        Database.PutParameter(dbCommand,"@rsi", rights.rsi);
      
        Database.PutParameter(dbCommand,"@rso", rights.rso);
      
        Database.PutParameter(dbCommand,"@rsl", rights.rsl);
      
        Database.PutParameter(dbCommand,"@rsv", rights.rsv);
      
        Database.PutParameter(dbCommand,"@rsd", rights.rsd);
      
        Database.PutParameter(dbCommand,"@rsea", rights.rsea);
      
        Database.PutParameter(dbCommand,"@rseb", rights.rseb);
      
        Database.PutParameter(dbCommand,"@rsec", rights.rsec);
      
        Database.PutParameter(dbCommand,"@rsed", rights.rsed);
      
        Database.PutParameter(dbCommand,"@rsee", rights.rsee);
      
        Database.PutParameter(dbCommand,"@rsef", rights.rsef);
      
        Database.PutParameter(dbCommand,"@rseg", rights.rseg);
      
        Database.PutParameter(dbCommand,"@rseh", rights.rseh);
      
        Database.PutParameter(dbCommand,"@rsei", rights.rsei);
      
        Database.PutParameter(dbCommand,"@rsem", rights.rsem);
      
        Database.PutParameter(dbCommand,"@rsen", rights.rsen);
      
        Database.PutParameter(dbCommand,"@rseo", rights.rseo);
      
        Database.PutParameter(dbCommand,"@rsep", rights.rsep);
      
        Database.PutParameter(dbCommand,"@rser", rights.rser);
      
        Database.PutParameter(dbCommand,"@rses", rights.rses);
      
        Database.PutParameter(dbCommand,"@rset", rights.rset);
      
        Database.PutParameter(dbCommand,"@rseu", rights.rseu);
      
        Database.PutParameter(dbCommand,"@rsev", rights.rsev);
      
        Database.PutParameter(dbCommand,"@rsex", rights.rsex);
      
        Database.PutParameter(dbCommand,"@rsey", rights.rsey);
      
        Database.PutParameter(dbCommand,"@rsel", rights.rsel);
      
        Database.PutParameter(dbCommand,"@rsez", rights.rsez);
      
        Database.PutParameter(dbCommand,"@rcell", rights.rcell);
      
        Database.PutParameter(dbCommand,"@rsysmenu", rights.rsysmenu);
      
        Database.PutParameter(dbCommand,"@sysadmin", rights.sysadmin);
      
        Database.PutParameter(dbCommand,"@old_id", rights.old_id);
      
        Database.PutParameter(dbCommand,"@savertime", rights.savertime);
      
        Database.PutParameter(dbCommand,"@localprt", rights.localprt);
      
        Database.PutParameter(dbCommand,"@rdc", rights.rdc);
      
        Database.PutParameter(dbCommand,"@ropr", rights.ropr);
      
        Database.PutParameter(dbCommand,"@rare", rights.rare);
      
        Database.PutParameter(dbCommand,"@rse1", rights.rse1);
      
        Database.PutParameter(dbCommand,"@compress", rights.compress);
      
        Database.PutParameter(dbCommand,"@rmru", rights.rmru);
      
        Database.PutParameter(dbCommand,"@rmrud", rights.rmrud);
      
        Database.PutParameter(dbCommand,"@rmrus", rights.rmrus);
      
        Database.PutParameter(dbCommand,"@rul", rights.rul);
      
        Database.PutParameter(dbCommand,"@rmrb", rights.rmrb);
      
        Database.PutParameter(dbCommand,"@rmrbt", rights.rmrbt);
      
        Database.PutParameter(dbCommand,"@rmrbc", rights.rmrbc);
      
        Database.PutParameter(dbCommand,"@rmrbd", rights.rmrbd);
      
        Database.PutParameter(dbCommand,"@rmrbcl", rights.rmrbcl);
      
        Database.PutParameter(dbCommand,"@rsh", rights.rsh);
      
        Database.PutParameter(dbCommand,"@pradmin", rights.pradmin);
      
        Database.PutParameter(dbCommand,"@unlimited", rights.unlimited);
      
        Database.PutParameter(dbCommand,"@rse2", rights.rse2);
      
        Database.PutParameter(dbCommand,"@rse3", rights.rse3);
      
        Database.PutParameter(dbCommand,"@rse4", rights.rse4);
      
        Database.PutParameter(dbCommand,"@rse5", rights.rse5);
      
        Database.PutParameter(dbCommand,"@rmr1", rights.rmr1);
      
        Database.PutParameter(dbCommand,"@rmi", rights.rmi);
      
        Database.PutParameter(dbCommand,"@rmim", rights.rmim);
      
        Database.PutParameter(dbCommand,"@rmir", rights.rmir);
      
        Database.PutParameter(dbCommand,"@rmis", rights.rmis);
      
        Database.PutParameter(dbCommand,"@rmird", rights.rmird);
      
        Database.PutParameter(dbCommand,"@rmirs", rights.rmirs);
      
        Database.PutParameter(dbCommand,"@ropl", rights.ropl);
      
        Database.PutParameter(dbCommand,"@ropt", rights.ropt);
      
        Database.PutParameter(dbCommand,"@rmr2", rights.rmr2);
      
        Database.PutParameter(dbCommand,"@rmr3", rights.rmr3);
      
        Database.PutParameter(dbCommand,"@rmrba", rights.rmrba);
      
        Database.PutParameter(dbCommand,"@roph", rights.roph);
      
        Database.PutParameter(dbCommand,"@rmr4", rights.rmr4);
      
        Database.PutParameter(dbCommand,"@rse6", rights.rse6);
      
        Database.PutParameter(dbCommand,"@company_id", rights.company_id);
      
        Database.PutParameter(dbCommand,"@dealer", rights.dealer);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<rights>  rightsList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(rights rights in  rightsList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@userid", rights.userid);
      
        Database.PutParameter(dbCommand,"@user", rights.user);
      
        Database.PutParameter(dbCommand,"@login_name", rights.login_name);
      
        Database.PutParameter(dbCommand,"@password", rights.password);
      
        Database.PutParameter(dbCommand,"@department", rights.department);
      
        Database.PutParameter(dbCommand,"@active", rights.active);
      
        Database.PutParameter(dbCommand,"@online", rights.online);
      
        Database.PutParameter(dbCommand,"@ro", rights.ro);
      
        Database.PutParameter(dbCommand,"@roc", rights.roc);
      
        Database.PutParameter(dbCommand,"@rom", rights.rom);
      
        Database.PutParameter(dbCommand,"@ron", rights.ron);
      
        Database.PutParameter(dbCommand,"@ror", rights.ror);
      
        Database.PutParameter(dbCommand,"@row", rights.row);
      
        Database.PutParameter(dbCommand,"@rou", rights.rou);
      
        Database.PutParameter(dbCommand,"@roua", rights.roua);
      
        Database.PutParameter(dbCommand,"@rouk", rights.rouk);
      
        Database.PutParameter(dbCommand,"@roud", rights.roud);
      
        Database.PutParameter(dbCommand,"@rouh", rights.rouh);
      
        Database.PutParameter(dbCommand,"@rouj", rights.rouj);
      
        Database.PutParameter(dbCommand,"@roul", rights.roul);
      
        Database.PutParameter(dbCommand,"@rour", rights.rour);
      
        Database.PutParameter(dbCommand,"@rous", rights.rous);
      
        Database.PutParameter(dbCommand,"@rouc", rights.rouc);
      
        Database.PutParameter(dbCommand,"@roum", rights.roum);
      
        Database.PutParameter(dbCommand,"@rout", rights.rout);
      
        Database.PutParameter(dbCommand,"@roux", rights.roux);
      
        Database.PutParameter(dbCommand,"@rop", rights.rop);
      
        Database.PutParameter(dbCommand,"@ropa", rights.ropa);
      
        Database.PutParameter(dbCommand,"@ropf", rights.ropf);
      
        Database.PutParameter(dbCommand,"@ropd", rights.ropd);
      
        Database.PutParameter(dbCommand,"@ropc", rights.ropc);
      
        Database.PutParameter(dbCommand,"@rops", rights.rops);
      
        Database.PutParameter(dbCommand,"@ropn", rights.ropn);
      
        Database.PutParameter(dbCommand,"@rd", rights.rd);
      
        Database.PutParameter(dbCommand,"@rde", rights.rde);
      
        Database.PutParameter(dbCommand,"@rdq", rights.rdq);
      
        Database.PutParameter(dbCommand,"@rdr", rights.rdr);
      
        Database.PutParameter(dbCommand,"@rds", rights.rds);
      
        Database.PutParameter(dbCommand,"@rdu", rights.rdu);
      
        Database.PutParameter(dbCommand,"@rdua", rights.rdua);
      
        Database.PutParameter(dbCommand,"@rduc", rights.rduc);
      
        Database.PutParameter(dbCommand,"@rdud", rights.rdud);
      
        Database.PutParameter(dbCommand,"@rdup", rights.rdup);
      
        Database.PutParameter(dbCommand,"@rdus", rights.rdus);
      
        Database.PutParameter(dbCommand,"@rdp", rights.rdp);
      
        Database.PutParameter(dbCommand,"@rdpc", rights.rdpc);
      
        Database.PutParameter(dbCommand,"@rdpd", rights.rdpd);
      
        Database.PutParameter(dbCommand,"@rdpi", rights.rdpi);
      
        Database.PutParameter(dbCommand,"@rdpl", rights.rdpl);
      
        Database.PutParameter(dbCommand,"@rdpo", rights.rdpo);
      
        Database.PutParameter(dbCommand,"@rdpp", rights.rdpp);
      
        Database.PutParameter(dbCommand,"@rdps", rights.rdps);
      
        Database.PutParameter(dbCommand,"@rdpb", rights.rdpb);
      
        Database.PutParameter(dbCommand,"@rdpn", rights.rdpn);
      
        Database.PutParameter(dbCommand,"@rdpq", rights.rdpq);
      
        Database.PutParameter(dbCommand,"@rdpu", rights.rdpu);
      
        Database.PutParameter(dbCommand,"@rdpa", rights.rdpa);
      
        Database.PutParameter(dbCommand,"@rdpaa", rights.rdpaa);
      
        Database.PutParameter(dbCommand,"@rdp1", rights.rdp1);
      
        Database.PutParameter(dbCommand,"@rdj", rights.rdj);
      
        Database.PutParameter(dbCommand,"@ra", rights.ra);
      
        Database.PutParameter(dbCommand,"@rac", rights.rac);
      
        Database.PutParameter(dbCommand,"@rap", rights.rap);
      
        Database.PutParameter(dbCommand,"@rapr", rights.rapr);
      
        Database.PutParameter(dbCommand,"@rapa", rights.rapa);
      
        Database.PutParameter(dbCommand,"@rapt", rights.rapt);
      
        Database.PutParameter(dbCommand,"@rapp", rights.rapp);
      
        Database.PutParameter(dbCommand,"@rapf", rights.rapf);
      
        Database.PutParameter(dbCommand,"@rapc", rights.rapc);
      
        Database.PutParameter(dbCommand,"@rapcb", rights.rapcb);
      
        Database.PutParameter(dbCommand,"@rapcbt", rights.rapcbt);
      
        Database.PutParameter(dbCommand,"@rapcbe", rights.rapcbe);
      
        Database.PutParameter(dbCommand,"@rapcbo", rights.rapcbo);
      
        Database.PutParameter(dbCommand,"@rapcba", rights.rapcba);
      
        Database.PutParameter(dbCommand,"@rapcbp", rights.rapcbp);
      
        Database.PutParameter(dbCommand,"@rapcbg", rights.rapcbg);
      
        Database.PutParameter(dbCommand,"@rapcbj", rights.rapcbj);
      
        Database.PutParameter(dbCommand,"@rapcbr", rights.rapcbr);
      
        Database.PutParameter(dbCommand,"@rapcbrp", rights.rapcbrp);
      
        Database.PutParameter(dbCommand,"@rapcp", rights.rapcp);
      
        Database.PutParameter(dbCommand,"@rapcps", rights.rapcps);
      
        Database.PutParameter(dbCommand,"@rapcpc", rights.rapcpc);
      
        Database.PutParameter(dbCommand,"@rapcpr", rights.rapcpr);
      
        Database.PutParameter(dbCommand,"@rapcprp", rights.rapcprp);
      
        Database.PutParameter(dbCommand,"@rau", rights.rau);
      
        Database.PutParameter(dbCommand,"@raua", rights.raua);
      
        Database.PutParameter(dbCommand,"@rauc", rights.rauc);
      
        Database.PutParameter(dbCommand,"@raue", rights.raue);
      
        Database.PutParameter(dbCommand,"@rauh", rights.rauh);
      
        Database.PutParameter(dbCommand,"@raus", rights.raus);
      
        Database.PutParameter(dbCommand,"@raum", rights.raum);
      
        Database.PutParameter(dbCommand,"@rar", rights.rar);
      
        Database.PutParameter(dbCommand,"@rar1", rights.rar1);
      
        Database.PutParameter(dbCommand,"@rar2", rights.rar2);
      
        Database.PutParameter(dbCommand,"@rara", rights.rara);
      
        Database.PutParameter(dbCommand,"@rars", rights.rars);
      
        Database.PutParameter(dbCommand,"@rard", rights.rard);
      
        Database.PutParameter(dbCommand,"@rarh", rights.rarh);
      
        Database.PutParameter(dbCommand,"@rarl", rights.rarl);
      
        Database.PutParameter(dbCommand,"@rarm", rights.rarm);
      
        Database.PutParameter(dbCommand,"@rarn", rights.rarn);
      
        Database.PutParameter(dbCommand,"@raru", rights.raru);
      
        Database.PutParameter(dbCommand,"@rarg", rights.rarg);
      
        Database.PutParameter(dbCommand,"@rarc", rights.rarc);
      
        Database.PutParameter(dbCommand,"@rarr", rights.rarr);
      
        Database.PutParameter(dbCommand,"@rarb", rights.rarb);
      
        Database.PutParameter(dbCommand,"@rart", rights.rart);
      
        Database.PutParameter(dbCommand,"@rari", rights.rari);
      
        Database.PutParameter(dbCommand,"@rarz", rights.rarz);
      
        Database.PutParameter(dbCommand,"@rark", rights.rark);
      
        Database.PutParameter(dbCommand,"@rarw", rights.rarw);
      
        Database.PutParameter(dbCommand,"@raro", rights.raro);
      
        Database.PutParameter(dbCommand,"@rarp", rights.rarp);
      
        Database.PutParameter(dbCommand,"@ru", rights.ru);
      
        Database.PutParameter(dbCommand,"@rui", rights.rui);
      
        Database.PutParameter(dbCommand,"@ruo", rights.ruo);
      
        Database.PutParameter(dbCommand,"@rue", rights.rue);
      
        Database.PutParameter(dbCommand,"@ruq", rights.ruq);
      
        Database.PutParameter(dbCommand,"@ruw", rights.ruw);
      
        Database.PutParameter(dbCommand,"@ruc", rights.ruc);
      
        Database.PutParameter(dbCommand,"@rum", rights.rum);
      
        Database.PutParameter(dbCommand,"@rus", rights.rus);
      
        Database.PutParameter(dbCommand,"@rm", rights.rm);
      
        Database.PutParameter(dbCommand,"@rmr", rights.rmr);
      
        Database.PutParameter(dbCommand,"@rmra", rights.rmra);
      
        Database.PutParameter(dbCommand,"@rmrs", rights.rmrs);
      
        Database.PutParameter(dbCommand,"@rmrd", rights.rmrd);
      
        Database.PutParameter(dbCommand,"@rmrm", rights.rmrm);
      
        Database.PutParameter(dbCommand,"@rmrn", rights.rmrn);
      
        Database.PutParameter(dbCommand,"@rmrr", rights.rmrr);
      
        Database.PutParameter(dbCommand,"@rmrk", rights.rmrk);
      
        Database.PutParameter(dbCommand,"@rmrt", rights.rmrt);
      
        Database.PutParameter(dbCommand,"@rmrj", rights.rmrj);
      
        Database.PutParameter(dbCommand,"@rmrw", rights.rmrw);
      
        Database.PutParameter(dbCommand,"@rmrv", rights.rmrv);
      
        Database.PutParameter(dbCommand,"@rmre", rights.rmre);
      
        Database.PutParameter(dbCommand,"@rmrp", rights.rmrp);
      
        Database.PutParameter(dbCommand,"@rmro", rights.rmro);
      
        Database.PutParameter(dbCommand,"@rmrl", rights.rmrl);
      
        Database.PutParameter(dbCommand,"@rmry", rights.rmry);
      
        Database.PutParameter(dbCommand,"@rmrc", rights.rmrc);
      
        Database.PutParameter(dbCommand,"@rmrf", rights.rmrf);
      
        Database.PutParameter(dbCommand,"@rmg", rights.rmg);
      
        Database.PutParameter(dbCommand,"@rmp", rights.rmp);
      
        Database.PutParameter(dbCommand,"@rs", rights.rs);
      
        Database.PutParameter(dbCommand,"@rss", rights.rss);
      
        Database.PutParameter(dbCommand,"@rssa", rights.rssa);
      
        Database.PutParameter(dbCommand,"@rsse", rights.rsse);
      
        Database.PutParameter(dbCommand,"@rssr", rights.rssr);
      
        Database.PutParameter(dbCommand,"@rssu", rights.rssu);
      
        Database.PutParameter(dbCommand,"@rssz", rights.rssz);
      
        Database.PutParameter(dbCommand,"@rse", rights.rse);
      
        Database.PutParameter(dbCommand,"@rsu", rights.rsu);
      
        Database.PutParameter(dbCommand,"@rsc", rights.rsc);
      
        Database.PutParameter(dbCommand,"@rsi", rights.rsi);
      
        Database.PutParameter(dbCommand,"@rso", rights.rso);
      
        Database.PutParameter(dbCommand,"@rsl", rights.rsl);
      
        Database.PutParameter(dbCommand,"@rsv", rights.rsv);
      
        Database.PutParameter(dbCommand,"@rsd", rights.rsd);
      
        Database.PutParameter(dbCommand,"@rsea", rights.rsea);
      
        Database.PutParameter(dbCommand,"@rseb", rights.rseb);
      
        Database.PutParameter(dbCommand,"@rsec", rights.rsec);
      
        Database.PutParameter(dbCommand,"@rsed", rights.rsed);
      
        Database.PutParameter(dbCommand,"@rsee", rights.rsee);
      
        Database.PutParameter(dbCommand,"@rsef", rights.rsef);
      
        Database.PutParameter(dbCommand,"@rseg", rights.rseg);
      
        Database.PutParameter(dbCommand,"@rseh", rights.rseh);
      
        Database.PutParameter(dbCommand,"@rsei", rights.rsei);
      
        Database.PutParameter(dbCommand,"@rsem", rights.rsem);
      
        Database.PutParameter(dbCommand,"@rsen", rights.rsen);
      
        Database.PutParameter(dbCommand,"@rseo", rights.rseo);
      
        Database.PutParameter(dbCommand,"@rsep", rights.rsep);
      
        Database.PutParameter(dbCommand,"@rser", rights.rser);
      
        Database.PutParameter(dbCommand,"@rses", rights.rses);
      
        Database.PutParameter(dbCommand,"@rset", rights.rset);
      
        Database.PutParameter(dbCommand,"@rseu", rights.rseu);
      
        Database.PutParameter(dbCommand,"@rsev", rights.rsev);
      
        Database.PutParameter(dbCommand,"@rsex", rights.rsex);
      
        Database.PutParameter(dbCommand,"@rsey", rights.rsey);
      
        Database.PutParameter(dbCommand,"@rsel", rights.rsel);
      
        Database.PutParameter(dbCommand,"@rsez", rights.rsez);
      
        Database.PutParameter(dbCommand,"@rcell", rights.rcell);
      
        Database.PutParameter(dbCommand,"@rsysmenu", rights.rsysmenu);
      
        Database.PutParameter(dbCommand,"@sysadmin", rights.sysadmin);
      
        Database.PutParameter(dbCommand,"@old_id", rights.old_id);
      
        Database.PutParameter(dbCommand,"@savertime", rights.savertime);
      
        Database.PutParameter(dbCommand,"@localprt", rights.localprt);
      
        Database.PutParameter(dbCommand,"@rdc", rights.rdc);
      
        Database.PutParameter(dbCommand,"@ropr", rights.ropr);
      
        Database.PutParameter(dbCommand,"@rare", rights.rare);
      
        Database.PutParameter(dbCommand,"@rse1", rights.rse1);
      
        Database.PutParameter(dbCommand,"@compress", rights.compress);
      
        Database.PutParameter(dbCommand,"@rmru", rights.rmru);
      
        Database.PutParameter(dbCommand,"@rmrud", rights.rmrud);
      
        Database.PutParameter(dbCommand,"@rmrus", rights.rmrus);
      
        Database.PutParameter(dbCommand,"@rul", rights.rul);
      
        Database.PutParameter(dbCommand,"@rmrb", rights.rmrb);
      
        Database.PutParameter(dbCommand,"@rmrbt", rights.rmrbt);
      
        Database.PutParameter(dbCommand,"@rmrbc", rights.rmrbc);
      
        Database.PutParameter(dbCommand,"@rmrbd", rights.rmrbd);
      
        Database.PutParameter(dbCommand,"@rmrbcl", rights.rmrbcl);
      
        Database.PutParameter(dbCommand,"@rsh", rights.rsh);
      
        Database.PutParameter(dbCommand,"@pradmin", rights.pradmin);
      
        Database.PutParameter(dbCommand,"@unlimited", rights.unlimited);
      
        Database.PutParameter(dbCommand,"@rse2", rights.rse2);
      
        Database.PutParameter(dbCommand,"@rse3", rights.rse3);
      
        Database.PutParameter(dbCommand,"@rse4", rights.rse4);
      
        Database.PutParameter(dbCommand,"@rse5", rights.rse5);
      
        Database.PutParameter(dbCommand,"@rmr1", rights.rmr1);
      
        Database.PutParameter(dbCommand,"@rmi", rights.rmi);
      
        Database.PutParameter(dbCommand,"@rmim", rights.rmim);
      
        Database.PutParameter(dbCommand,"@rmir", rights.rmir);
      
        Database.PutParameter(dbCommand,"@rmis", rights.rmis);
      
        Database.PutParameter(dbCommand,"@rmird", rights.rmird);
      
        Database.PutParameter(dbCommand,"@rmirs", rights.rmirs);
      
        Database.PutParameter(dbCommand,"@ropl", rights.ropl);
      
        Database.PutParameter(dbCommand,"@ropt", rights.ropt);
      
        Database.PutParameter(dbCommand,"@rmr2", rights.rmr2);
      
        Database.PutParameter(dbCommand,"@rmr3", rights.rmr3);
      
        Database.PutParameter(dbCommand,"@rmrba", rights.rmrba);
      
        Database.PutParameter(dbCommand,"@roph", rights.roph);
      
        Database.PutParameter(dbCommand,"@rmr4", rights.rmr4);
      
        Database.PutParameter(dbCommand,"@rse6", rights.rse6);
      
        Database.PutParameter(dbCommand,"@company_id", rights.company_id);
      
        Database.PutParameter(dbCommand,"@dealer", rights.dealer);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@userid",rights.userid);
      
        Database.UpdateParameter(dbCommand,"@user",rights.user);
      
        Database.UpdateParameter(dbCommand,"@login_name",rights.login_name);
      
        Database.UpdateParameter(dbCommand,"@password",rights.password);
      
        Database.UpdateParameter(dbCommand,"@department",rights.department);
      
        Database.UpdateParameter(dbCommand,"@active",rights.active);
      
        Database.UpdateParameter(dbCommand,"@online",rights.online);
      
        Database.UpdateParameter(dbCommand,"@ro",rights.ro);
      
        Database.UpdateParameter(dbCommand,"@roc",rights.roc);
      
        Database.UpdateParameter(dbCommand,"@rom",rights.rom);
      
        Database.UpdateParameter(dbCommand,"@ron",rights.ron);
      
        Database.UpdateParameter(dbCommand,"@ror",rights.ror);
      
        Database.UpdateParameter(dbCommand,"@row",rights.row);
      
        Database.UpdateParameter(dbCommand,"@rou",rights.rou);
      
        Database.UpdateParameter(dbCommand,"@roua",rights.roua);
      
        Database.UpdateParameter(dbCommand,"@rouk",rights.rouk);
      
        Database.UpdateParameter(dbCommand,"@roud",rights.roud);
      
        Database.UpdateParameter(dbCommand,"@rouh",rights.rouh);
      
        Database.UpdateParameter(dbCommand,"@rouj",rights.rouj);
      
        Database.UpdateParameter(dbCommand,"@roul",rights.roul);
      
        Database.UpdateParameter(dbCommand,"@rour",rights.rour);
      
        Database.UpdateParameter(dbCommand,"@rous",rights.rous);
      
        Database.UpdateParameter(dbCommand,"@rouc",rights.rouc);
      
        Database.UpdateParameter(dbCommand,"@roum",rights.roum);
      
        Database.UpdateParameter(dbCommand,"@rout",rights.rout);
      
        Database.UpdateParameter(dbCommand,"@roux",rights.roux);
      
        Database.UpdateParameter(dbCommand,"@rop",rights.rop);
      
        Database.UpdateParameter(dbCommand,"@ropa",rights.ropa);
      
        Database.UpdateParameter(dbCommand,"@ropf",rights.ropf);
      
        Database.UpdateParameter(dbCommand,"@ropd",rights.ropd);
      
        Database.UpdateParameter(dbCommand,"@ropc",rights.ropc);
      
        Database.UpdateParameter(dbCommand,"@rops",rights.rops);
      
        Database.UpdateParameter(dbCommand,"@ropn",rights.ropn);
      
        Database.UpdateParameter(dbCommand,"@rd",rights.rd);
      
        Database.UpdateParameter(dbCommand,"@rde",rights.rde);
      
        Database.UpdateParameter(dbCommand,"@rdq",rights.rdq);
      
        Database.UpdateParameter(dbCommand,"@rdr",rights.rdr);
      
        Database.UpdateParameter(dbCommand,"@rds",rights.rds);
      
        Database.UpdateParameter(dbCommand,"@rdu",rights.rdu);
      
        Database.UpdateParameter(dbCommand,"@rdua",rights.rdua);
      
        Database.UpdateParameter(dbCommand,"@rduc",rights.rduc);
      
        Database.UpdateParameter(dbCommand,"@rdud",rights.rdud);
      
        Database.UpdateParameter(dbCommand,"@rdup",rights.rdup);
      
        Database.UpdateParameter(dbCommand,"@rdus",rights.rdus);
      
        Database.UpdateParameter(dbCommand,"@rdp",rights.rdp);
      
        Database.UpdateParameter(dbCommand,"@rdpc",rights.rdpc);
      
        Database.UpdateParameter(dbCommand,"@rdpd",rights.rdpd);
      
        Database.UpdateParameter(dbCommand,"@rdpi",rights.rdpi);
      
        Database.UpdateParameter(dbCommand,"@rdpl",rights.rdpl);
      
        Database.UpdateParameter(dbCommand,"@rdpo",rights.rdpo);
      
        Database.UpdateParameter(dbCommand,"@rdpp",rights.rdpp);
      
        Database.UpdateParameter(dbCommand,"@rdps",rights.rdps);
      
        Database.UpdateParameter(dbCommand,"@rdpb",rights.rdpb);
      
        Database.UpdateParameter(dbCommand,"@rdpn",rights.rdpn);
      
        Database.UpdateParameter(dbCommand,"@rdpq",rights.rdpq);
      
        Database.UpdateParameter(dbCommand,"@rdpu",rights.rdpu);
      
        Database.UpdateParameter(dbCommand,"@rdpa",rights.rdpa);
      
        Database.UpdateParameter(dbCommand,"@rdpaa",rights.rdpaa);
      
        Database.UpdateParameter(dbCommand,"@rdp1",rights.rdp1);
      
        Database.UpdateParameter(dbCommand,"@rdj",rights.rdj);
      
        Database.UpdateParameter(dbCommand,"@ra",rights.ra);
      
        Database.UpdateParameter(dbCommand,"@rac",rights.rac);
      
        Database.UpdateParameter(dbCommand,"@rap",rights.rap);
      
        Database.UpdateParameter(dbCommand,"@rapr",rights.rapr);
      
        Database.UpdateParameter(dbCommand,"@rapa",rights.rapa);
      
        Database.UpdateParameter(dbCommand,"@rapt",rights.rapt);
      
        Database.UpdateParameter(dbCommand,"@rapp",rights.rapp);
      
        Database.UpdateParameter(dbCommand,"@rapf",rights.rapf);
      
        Database.UpdateParameter(dbCommand,"@rapc",rights.rapc);
      
        Database.UpdateParameter(dbCommand,"@rapcb",rights.rapcb);
      
        Database.UpdateParameter(dbCommand,"@rapcbt",rights.rapcbt);
      
        Database.UpdateParameter(dbCommand,"@rapcbe",rights.rapcbe);
      
        Database.UpdateParameter(dbCommand,"@rapcbo",rights.rapcbo);
      
        Database.UpdateParameter(dbCommand,"@rapcba",rights.rapcba);
      
        Database.UpdateParameter(dbCommand,"@rapcbp",rights.rapcbp);
      
        Database.UpdateParameter(dbCommand,"@rapcbg",rights.rapcbg);
      
        Database.UpdateParameter(dbCommand,"@rapcbj",rights.rapcbj);
      
        Database.UpdateParameter(dbCommand,"@rapcbr",rights.rapcbr);
      
        Database.UpdateParameter(dbCommand,"@rapcbrp",rights.rapcbrp);
      
        Database.UpdateParameter(dbCommand,"@rapcp",rights.rapcp);
      
        Database.UpdateParameter(dbCommand,"@rapcps",rights.rapcps);
      
        Database.UpdateParameter(dbCommand,"@rapcpc",rights.rapcpc);
      
        Database.UpdateParameter(dbCommand,"@rapcpr",rights.rapcpr);
      
        Database.UpdateParameter(dbCommand,"@rapcprp",rights.rapcprp);
      
        Database.UpdateParameter(dbCommand,"@rau",rights.rau);
      
        Database.UpdateParameter(dbCommand,"@raua",rights.raua);
      
        Database.UpdateParameter(dbCommand,"@rauc",rights.rauc);
      
        Database.UpdateParameter(dbCommand,"@raue",rights.raue);
      
        Database.UpdateParameter(dbCommand,"@rauh",rights.rauh);
      
        Database.UpdateParameter(dbCommand,"@raus",rights.raus);
      
        Database.UpdateParameter(dbCommand,"@raum",rights.raum);
      
        Database.UpdateParameter(dbCommand,"@rar",rights.rar);
      
        Database.UpdateParameter(dbCommand,"@rar1",rights.rar1);
      
        Database.UpdateParameter(dbCommand,"@rar2",rights.rar2);
      
        Database.UpdateParameter(dbCommand,"@rara",rights.rara);
      
        Database.UpdateParameter(dbCommand,"@rars",rights.rars);
      
        Database.UpdateParameter(dbCommand,"@rard",rights.rard);
      
        Database.UpdateParameter(dbCommand,"@rarh",rights.rarh);
      
        Database.UpdateParameter(dbCommand,"@rarl",rights.rarl);
      
        Database.UpdateParameter(dbCommand,"@rarm",rights.rarm);
      
        Database.UpdateParameter(dbCommand,"@rarn",rights.rarn);
      
        Database.UpdateParameter(dbCommand,"@raru",rights.raru);
      
        Database.UpdateParameter(dbCommand,"@rarg",rights.rarg);
      
        Database.UpdateParameter(dbCommand,"@rarc",rights.rarc);
      
        Database.UpdateParameter(dbCommand,"@rarr",rights.rarr);
      
        Database.UpdateParameter(dbCommand,"@rarb",rights.rarb);
      
        Database.UpdateParameter(dbCommand,"@rart",rights.rart);
      
        Database.UpdateParameter(dbCommand,"@rari",rights.rari);
      
        Database.UpdateParameter(dbCommand,"@rarz",rights.rarz);
      
        Database.UpdateParameter(dbCommand,"@rark",rights.rark);
      
        Database.UpdateParameter(dbCommand,"@rarw",rights.rarw);
      
        Database.UpdateParameter(dbCommand,"@raro",rights.raro);
      
        Database.UpdateParameter(dbCommand,"@rarp",rights.rarp);
      
        Database.UpdateParameter(dbCommand,"@ru",rights.ru);
      
        Database.UpdateParameter(dbCommand,"@rui",rights.rui);
      
        Database.UpdateParameter(dbCommand,"@ruo",rights.ruo);
      
        Database.UpdateParameter(dbCommand,"@rue",rights.rue);
      
        Database.UpdateParameter(dbCommand,"@ruq",rights.ruq);
      
        Database.UpdateParameter(dbCommand,"@ruw",rights.ruw);
      
        Database.UpdateParameter(dbCommand,"@ruc",rights.ruc);
      
        Database.UpdateParameter(dbCommand,"@rum",rights.rum);
      
        Database.UpdateParameter(dbCommand,"@rus",rights.rus);
      
        Database.UpdateParameter(dbCommand,"@rm",rights.rm);
      
        Database.UpdateParameter(dbCommand,"@rmr",rights.rmr);
      
        Database.UpdateParameter(dbCommand,"@rmra",rights.rmra);
      
        Database.UpdateParameter(dbCommand,"@rmrs",rights.rmrs);
      
        Database.UpdateParameter(dbCommand,"@rmrd",rights.rmrd);
      
        Database.UpdateParameter(dbCommand,"@rmrm",rights.rmrm);
      
        Database.UpdateParameter(dbCommand,"@rmrn",rights.rmrn);
      
        Database.UpdateParameter(dbCommand,"@rmrr",rights.rmrr);
      
        Database.UpdateParameter(dbCommand,"@rmrk",rights.rmrk);
      
        Database.UpdateParameter(dbCommand,"@rmrt",rights.rmrt);
      
        Database.UpdateParameter(dbCommand,"@rmrj",rights.rmrj);
      
        Database.UpdateParameter(dbCommand,"@rmrw",rights.rmrw);
      
        Database.UpdateParameter(dbCommand,"@rmrv",rights.rmrv);
      
        Database.UpdateParameter(dbCommand,"@rmre",rights.rmre);
      
        Database.UpdateParameter(dbCommand,"@rmrp",rights.rmrp);
      
        Database.UpdateParameter(dbCommand,"@rmro",rights.rmro);
      
        Database.UpdateParameter(dbCommand,"@rmrl",rights.rmrl);
      
        Database.UpdateParameter(dbCommand,"@rmry",rights.rmry);
      
        Database.UpdateParameter(dbCommand,"@rmrc",rights.rmrc);
      
        Database.UpdateParameter(dbCommand,"@rmrf",rights.rmrf);
      
        Database.UpdateParameter(dbCommand,"@rmg",rights.rmg);
      
        Database.UpdateParameter(dbCommand,"@rmp",rights.rmp);
      
        Database.UpdateParameter(dbCommand,"@rs",rights.rs);
      
        Database.UpdateParameter(dbCommand,"@rss",rights.rss);
      
        Database.UpdateParameter(dbCommand,"@rssa",rights.rssa);
      
        Database.UpdateParameter(dbCommand,"@rsse",rights.rsse);
      
        Database.UpdateParameter(dbCommand,"@rssr",rights.rssr);
      
        Database.UpdateParameter(dbCommand,"@rssu",rights.rssu);
      
        Database.UpdateParameter(dbCommand,"@rssz",rights.rssz);
      
        Database.UpdateParameter(dbCommand,"@rse",rights.rse);
      
        Database.UpdateParameter(dbCommand,"@rsu",rights.rsu);
      
        Database.UpdateParameter(dbCommand,"@rsc",rights.rsc);
      
        Database.UpdateParameter(dbCommand,"@rsi",rights.rsi);
      
        Database.UpdateParameter(dbCommand,"@rso",rights.rso);
      
        Database.UpdateParameter(dbCommand,"@rsl",rights.rsl);
      
        Database.UpdateParameter(dbCommand,"@rsv",rights.rsv);
      
        Database.UpdateParameter(dbCommand,"@rsd",rights.rsd);
      
        Database.UpdateParameter(dbCommand,"@rsea",rights.rsea);
      
        Database.UpdateParameter(dbCommand,"@rseb",rights.rseb);
      
        Database.UpdateParameter(dbCommand,"@rsec",rights.rsec);
      
        Database.UpdateParameter(dbCommand,"@rsed",rights.rsed);
      
        Database.UpdateParameter(dbCommand,"@rsee",rights.rsee);
      
        Database.UpdateParameter(dbCommand,"@rsef",rights.rsef);
      
        Database.UpdateParameter(dbCommand,"@rseg",rights.rseg);
      
        Database.UpdateParameter(dbCommand,"@rseh",rights.rseh);
      
        Database.UpdateParameter(dbCommand,"@rsei",rights.rsei);
      
        Database.UpdateParameter(dbCommand,"@rsem",rights.rsem);
      
        Database.UpdateParameter(dbCommand,"@rsen",rights.rsen);
      
        Database.UpdateParameter(dbCommand,"@rseo",rights.rseo);
      
        Database.UpdateParameter(dbCommand,"@rsep",rights.rsep);
      
        Database.UpdateParameter(dbCommand,"@rser",rights.rser);
      
        Database.UpdateParameter(dbCommand,"@rses",rights.rses);
      
        Database.UpdateParameter(dbCommand,"@rset",rights.rset);
      
        Database.UpdateParameter(dbCommand,"@rseu",rights.rseu);
      
        Database.UpdateParameter(dbCommand,"@rsev",rights.rsev);
      
        Database.UpdateParameter(dbCommand,"@rsex",rights.rsex);
      
        Database.UpdateParameter(dbCommand,"@rsey",rights.rsey);
      
        Database.UpdateParameter(dbCommand,"@rsel",rights.rsel);
      
        Database.UpdateParameter(dbCommand,"@rsez",rights.rsez);
      
        Database.UpdateParameter(dbCommand,"@rcell",rights.rcell);
      
        Database.UpdateParameter(dbCommand,"@rsysmenu",rights.rsysmenu);
      
        Database.UpdateParameter(dbCommand,"@sysadmin",rights.sysadmin);
      
        Database.UpdateParameter(dbCommand,"@old_id",rights.old_id);
      
        Database.UpdateParameter(dbCommand,"@savertime",rights.savertime);
      
        Database.UpdateParameter(dbCommand,"@localprt",rights.localprt);
      
        Database.UpdateParameter(dbCommand,"@rdc",rights.rdc);
      
        Database.UpdateParameter(dbCommand,"@ropr",rights.ropr);
      
        Database.UpdateParameter(dbCommand,"@rare",rights.rare);
      
        Database.UpdateParameter(dbCommand,"@rse1",rights.rse1);
      
        Database.UpdateParameter(dbCommand,"@compress",rights.compress);
      
        Database.UpdateParameter(dbCommand,"@rmru",rights.rmru);
      
        Database.UpdateParameter(dbCommand,"@rmrud",rights.rmrud);
      
        Database.UpdateParameter(dbCommand,"@rmrus",rights.rmrus);
      
        Database.UpdateParameter(dbCommand,"@rul",rights.rul);
      
        Database.UpdateParameter(dbCommand,"@rmrb",rights.rmrb);
      
        Database.UpdateParameter(dbCommand,"@rmrbt",rights.rmrbt);
      
        Database.UpdateParameter(dbCommand,"@rmrbc",rights.rmrbc);
      
        Database.UpdateParameter(dbCommand,"@rmrbd",rights.rmrbd);
      
        Database.UpdateParameter(dbCommand,"@rmrbcl",rights.rmrbcl);
      
        Database.UpdateParameter(dbCommand,"@rsh",rights.rsh);
      
        Database.UpdateParameter(dbCommand,"@pradmin",rights.pradmin);
      
        Database.UpdateParameter(dbCommand,"@unlimited",rights.unlimited);
      
        Database.UpdateParameter(dbCommand,"@rse2",rights.rse2);
      
        Database.UpdateParameter(dbCommand,"@rse3",rights.rse3);
      
        Database.UpdateParameter(dbCommand,"@rse4",rights.rse4);
      
        Database.UpdateParameter(dbCommand,"@rse5",rights.rse5);
      
        Database.UpdateParameter(dbCommand,"@rmr1",rights.rmr1);
      
        Database.UpdateParameter(dbCommand,"@rmi",rights.rmi);
      
        Database.UpdateParameter(dbCommand,"@rmim",rights.rmim);
      
        Database.UpdateParameter(dbCommand,"@rmir",rights.rmir);
      
        Database.UpdateParameter(dbCommand,"@rmis",rights.rmis);
      
        Database.UpdateParameter(dbCommand,"@rmird",rights.rmird);
      
        Database.UpdateParameter(dbCommand,"@rmirs",rights.rmirs);
      
        Database.UpdateParameter(dbCommand,"@ropl",rights.ropl);
      
        Database.UpdateParameter(dbCommand,"@ropt",rights.ropt);
      
        Database.UpdateParameter(dbCommand,"@rmr2",rights.rmr2);
      
        Database.UpdateParameter(dbCommand,"@rmr3",rights.rmr3);
      
        Database.UpdateParameter(dbCommand,"@rmrba",rights.rmrba);
      
        Database.UpdateParameter(dbCommand,"@roph",rights.roph);
      
        Database.UpdateParameter(dbCommand,"@rmr4",rights.rmr4);
      
        Database.UpdateParameter(dbCommand,"@rse6",rights.rse6);
      
        Database.UpdateParameter(dbCommand,"@company_id",rights.company_id);
      
        Database.UpdateParameter(dbCommand,"@dealer",rights.dealer);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update rights Set "
      
        + " rights.user = ? , "
      
        + " rights.login_name = ? , "
      
        + " rights.password = ? , "
      
        + " rights.department = ? , "
      
        + " rights.active = ? , "
      
        + " rights.online = ? , "
      
        + " rights.ro = ? , "
      
        + " rights.roc = ? , "
      
        + " rights.rom = ? , "
      
        + " rights.ron = ? , "
      
        + " rights.ror = ? , "
      
        + " rights.row = ? , "
      
        + " rights.rou = ? , "
      
        + " rights.roua = ? , "
      
        + " rights.rouk = ? , "
      
        + " rights.roud = ? , "
      
        + " rights.rouh = ? , "
      
        + " rights.rouj = ? , "
      
        + " rights.roul = ? , "
      
        + " rights.rour = ? , "
      
        + " rights.rous = ? , "
      
        + " rights.rouc = ? , "
      
        + " rights.roum = ? , "
      
        + " rights.rout = ? , "
      
        + " rights.roux = ? , "
      
        + " rights.rop = ? , "
      
        + " rights.ropa = ? , "
      
        + " rights.ropf = ? , "
      
        + " rights.ropd = ? , "
      
        + " rights.ropc = ? , "
      
        + " rights.rops = ? , "
      
        + " rights.ropn = ? , "
      
        + " rights.rd = ? , "
      
        + " rights.rde = ? , "
      
        + " rights.rdq = ? , "
      
        + " rights.rdr = ? , "
      
        + " rights.rds = ? , "
      
        + " rights.rdu = ? , "
      
        + " rights.rdua = ? , "
      
        + " rights.rduc = ? , "
      
        + " rights.rdud = ? , "
      
        + " rights.rdup = ? , "
      
        + " rights.rdus = ? , "
      
        + " rights.rdp = ? , "
      
        + " rights.rdpc = ? , "
      
        + " rights.rdpd = ? , "
      
        + " rights.rdpi = ? , "
      
        + " rights.rdpl = ? , "
      
        + " rights.rdpo = ? , "
      
        + " rights.rdpp = ? , "
      
        + " rights.rdps = ? , "
      
        + " rights.rdpb = ? , "
      
        + " rights.rdpn = ? , "
      
        + " rights.rdpq = ? , "
      
        + " rights.rdpu = ? , "
      
        + " rights.rdpa = ? , "
      
        + " rights.rdpaa = ? , "
      
        + " rights.rdp1 = ? , "
      
        + " rights.rdj = ? , "
      
        + " rights.ra = ? , "
      
        + " rights.rac = ? , "
      
        + " rights.rap = ? , "
      
        + " rights.rapr = ? , "
      
        + " rights.rapa = ? , "
      
        + " rights.rapt = ? , "
      
        + " rights.rapp = ? , "
      
        + " rights.rapf = ? , "
      
        + " rights.rapc = ? , "
      
        + " rights.rapcb = ? , "
      
        + " rights.rapcbt = ? , "
      
        + " rights.rapcbe = ? , "
      
        + " rights.rapcbo = ? , "
      
        + " rights.rapcba = ? , "
      
        + " rights.rapcbp = ? , "
      
        + " rights.rapcbg = ? , "
      
        + " rights.rapcbj = ? , "
      
        + " rights.rapcbr = ? , "
      
        + " rights.rapcbrp = ? , "
      
        + " rights.rapcp = ? , "
      
        + " rights.rapcps = ? , "
      
        + " rights.rapcpc = ? , "
      
        + " rights.rapcpr = ? , "
      
        + " rights.rapcprp = ? , "
      
        + " rights.rau = ? , "
      
        + " rights.raua = ? , "
      
        + " rights.rauc = ? , "
      
        + " rights.raue = ? , "
      
        + " rights.rauh = ? , "
      
        + " rights.raus = ? , "
      
        + " rights.raum = ? , "
      
        + " rights.rar = ? , "
      
        + " rights.rar1 = ? , "
      
        + " rights.rar2 = ? , "
      
        + " rights.rara = ? , "
      
        + " rights.rars = ? , "
      
        + " rights.rard = ? , "
      
        + " rights.rarh = ? , "
      
        + " rights.rarl = ? , "
      
        + " rights.rarm = ? , "
      
        + " rights.rarn = ? , "
      
        + " rights.raru = ? , "
      
        + " rights.rarg = ? , "
      
        + " rights.rarc = ? , "
      
        + " rights.rarr = ? , "
      
        + " rights.rarb = ? , "
      
        + " rights.rart = ? , "
      
        + " rights.rari = ? , "
      
        + " rights.rarz = ? , "
      
        + " rights.rark = ? , "
      
        + " rights.rarw = ? , "
      
        + " rights.raro = ? , "
      
        + " rights.rarp = ? , "
      
        + " rights.ru = ? , "
      
        + " rights.rui = ? , "
      
        + " rights.ruo = ? , "
      
        + " rights.rue = ? , "
      
        + " rights.ruq = ? , "
      
        + " rights.ruw = ? , "
      
        + " rights.ruc = ? , "
      
        + " rights.rum = ? , "
      
        + " rights.rus = ? , "
      
        + " rights.rm = ? , "
      
        + " rights.rmr = ? , "
      
        + " rights.rmra = ? , "
      
        + " rights.rmrs = ? , "
      
        + " rights.rmrd = ? , "
      
        + " rights.rmrm = ? , "
      
        + " rights.rmrn = ? , "
      
        + " rights.rmrr = ? , "
      
        + " rights.rmrk = ? , "
      
        + " rights.rmrt = ? , "
      
        + " rights.rmrj = ? , "
      
        + " rights.rmrw = ? , "
      
        + " rights.rmrv = ? , "
      
        + " rights.rmre = ? , "
      
        + " rights.rmrp = ? , "
      
        + " rights.rmro = ? , "
      
        + " rights.rmrl = ? , "
      
        + " rights.rmry = ? , "
      
        + " rights.rmrc = ? , "
      
        + " rights.rmrf = ? , "
      
        + " rights.rmg = ? , "
      
        + " rights.rmp = ? , "
      
        + " rights.rs = ? , "
      
        + " rights.rss = ? , "
      
        + " rights.rssa = ? , "
      
        + " rights.rsse = ? , "
      
        + " rights.rssr = ? , "
      
        + " rights.rssu = ? , "
      
        + " rights.rssz = ? , "
      
        + " rights.rse = ? , "
      
        + " rights.rsu = ? , "
      
        + " rights.rsc = ? , "
      
        + " rights.rsi = ? , "
      
        + " rights.rso = ? , "
      
        + " rights.rsl = ? , "
      
        + " rights.rsv = ? , "
      
        + " rights.rsd = ? , "
      
        + " rights.rsea = ? , "
      
        + " rights.rseb = ? , "
      
        + " rights.rsec = ? , "
      
        + " rights.rsed = ? , "
      
        + " rights.rsee = ? , "
      
        + " rights.rsef = ? , "
      
        + " rights.rseg = ? , "
      
        + " rights.rseh = ? , "
      
        + " rights.rsei = ? , "
      
        + " rights.rsem = ? , "
      
        + " rights.rsen = ? , "
      
        + " rights.rseo = ? , "
      
        + " rights.rsep = ? , "
      
        + " rights.rser = ? , "
      
        + " rights.rses = ? , "
      
        + " rights.rset = ? , "
      
        + " rights.rseu = ? , "
      
        + " rights.rsev = ? , "
      
        + " rights.rsex = ? , "
      
        + " rights.rsey = ? , "
      
        + " rights.rsel = ? , "
      
        + " rights.rsez = ? , "
      
        + " rights.rcell = ? , "
      
        + " rights.rsysmenu = ? , "
      
        + " rights.sysadmin = ? , "
      
        + " rights.old_id = ? , "
      
        + " rights.savertime = ? , "
      
        + " rights.localprt = ? , "
      
        + " rights.rdc = ? , "
      
        + " rights.ropr = ? , "
      
        + " rights.rare = ? , "
      
        + " rights.rse1 = ? , "
      
        + " rights.compress = ? , "
      
        + " rights.rmru = ? , "
      
        + " rights.rmrud = ? , "
      
        + " rights.rmrus = ? , "
      
        + " rights.rul = ? , "
      
        + " rights.rmrb = ? , "
      
        + " rights.rmrbt = ? , "
      
        + " rights.rmrbc = ? , "
      
        + " rights.rmrbd = ? , "
      
        + " rights.rmrbcl = ? , "
      
        + " rights.rsh = ? , "
      
        + " rights.pradmin = ? , "
      
        + " rights.unlimited = ? , "
      
        + " rights.rse2 = ? , "
      
        + " rights.rse3 = ? , "
      
        + " rights.rse4 = ? , "
      
        + " rights.rse5 = ? , "
      
        + " rights.rmr1 = ? , "
      
        + " rights.rmi = ? , "
      
        + " rights.rmim = ? , "
      
        + " rights.rmir = ? , "
      
        + " rights.rmis = ? , "
      
        + " rights.rmird = ? , "
      
        + " rights.rmirs = ? , "
      
        + " rights.ropl = ? , "
      
        + " rights.ropt = ? , "
      
        + " rights.rmr2 = ? , "
      
        + " rights.rmr3 = ? , "
      
        + " rights.rmrba = ? , "
      
        + " rights.roph = ? , "
      
        + " rights.rmr4 = ? , "
      
        + " rights.rse6 = ? , "
      
        + " rights.company_id = ? , "
      
        + " rights.dealer = ?  "
      
        + " Where "
        
          + " rights.userid = ?  "
        
      ;

      public static void Update(rights rights)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@user", rights.user);
      
        Database.PutParameter(dbCommand,"@login_name", rights.login_name);
      
        Database.PutParameter(dbCommand,"@password", rights.password);
      
        Database.PutParameter(dbCommand,"@department", rights.department);
      
        Database.PutParameter(dbCommand,"@active", rights.active);
      
        Database.PutParameter(dbCommand,"@online", rights.online);
      
        Database.PutParameter(dbCommand,"@ro", rights.ro);
      
        Database.PutParameter(dbCommand,"@roc", rights.roc);
      
        Database.PutParameter(dbCommand,"@rom", rights.rom);
      
        Database.PutParameter(dbCommand,"@ron", rights.ron);
      
        Database.PutParameter(dbCommand,"@ror", rights.ror);
      
        Database.PutParameter(dbCommand,"@row", rights.row);
      
        Database.PutParameter(dbCommand,"@rou", rights.rou);
      
        Database.PutParameter(dbCommand,"@roua", rights.roua);
      
        Database.PutParameter(dbCommand,"@rouk", rights.rouk);
      
        Database.PutParameter(dbCommand,"@roud", rights.roud);
      
        Database.PutParameter(dbCommand,"@rouh", rights.rouh);
      
        Database.PutParameter(dbCommand,"@rouj", rights.rouj);
      
        Database.PutParameter(dbCommand,"@roul", rights.roul);
      
        Database.PutParameter(dbCommand,"@rour", rights.rour);
      
        Database.PutParameter(dbCommand,"@rous", rights.rous);
      
        Database.PutParameter(dbCommand,"@rouc", rights.rouc);
      
        Database.PutParameter(dbCommand,"@roum", rights.roum);
      
        Database.PutParameter(dbCommand,"@rout", rights.rout);
      
        Database.PutParameter(dbCommand,"@roux", rights.roux);
      
        Database.PutParameter(dbCommand,"@rop", rights.rop);
      
        Database.PutParameter(dbCommand,"@ropa", rights.ropa);
      
        Database.PutParameter(dbCommand,"@ropf", rights.ropf);
      
        Database.PutParameter(dbCommand,"@ropd", rights.ropd);
      
        Database.PutParameter(dbCommand,"@ropc", rights.ropc);
      
        Database.PutParameter(dbCommand,"@rops", rights.rops);
      
        Database.PutParameter(dbCommand,"@ropn", rights.ropn);
      
        Database.PutParameter(dbCommand,"@rd", rights.rd);
      
        Database.PutParameter(dbCommand,"@rde", rights.rde);
      
        Database.PutParameter(dbCommand,"@rdq", rights.rdq);
      
        Database.PutParameter(dbCommand,"@rdr", rights.rdr);
      
        Database.PutParameter(dbCommand,"@rds", rights.rds);
      
        Database.PutParameter(dbCommand,"@rdu", rights.rdu);
      
        Database.PutParameter(dbCommand,"@rdua", rights.rdua);
      
        Database.PutParameter(dbCommand,"@rduc", rights.rduc);
      
        Database.PutParameter(dbCommand,"@rdud", rights.rdud);
      
        Database.PutParameter(dbCommand,"@rdup", rights.rdup);
      
        Database.PutParameter(dbCommand,"@rdus", rights.rdus);
      
        Database.PutParameter(dbCommand,"@rdp", rights.rdp);
      
        Database.PutParameter(dbCommand,"@rdpc", rights.rdpc);
      
        Database.PutParameter(dbCommand,"@rdpd", rights.rdpd);
      
        Database.PutParameter(dbCommand,"@rdpi", rights.rdpi);
      
        Database.PutParameter(dbCommand,"@rdpl", rights.rdpl);
      
        Database.PutParameter(dbCommand,"@rdpo", rights.rdpo);
      
        Database.PutParameter(dbCommand,"@rdpp", rights.rdpp);
      
        Database.PutParameter(dbCommand,"@rdps", rights.rdps);
      
        Database.PutParameter(dbCommand,"@rdpb", rights.rdpb);
      
        Database.PutParameter(dbCommand,"@rdpn", rights.rdpn);
      
        Database.PutParameter(dbCommand,"@rdpq", rights.rdpq);
      
        Database.PutParameter(dbCommand,"@rdpu", rights.rdpu);
      
        Database.PutParameter(dbCommand,"@rdpa", rights.rdpa);
      
        Database.PutParameter(dbCommand,"@rdpaa", rights.rdpaa);
      
        Database.PutParameter(dbCommand,"@rdp1", rights.rdp1);
      
        Database.PutParameter(dbCommand,"@rdj", rights.rdj);
      
        Database.PutParameter(dbCommand,"@ra", rights.ra);
      
        Database.PutParameter(dbCommand,"@rac", rights.rac);
      
        Database.PutParameter(dbCommand,"@rap", rights.rap);
      
        Database.PutParameter(dbCommand,"@rapr", rights.rapr);
      
        Database.PutParameter(dbCommand,"@rapa", rights.rapa);
      
        Database.PutParameter(dbCommand,"@rapt", rights.rapt);
      
        Database.PutParameter(dbCommand,"@rapp", rights.rapp);
      
        Database.PutParameter(dbCommand,"@rapf", rights.rapf);
      
        Database.PutParameter(dbCommand,"@rapc", rights.rapc);
      
        Database.PutParameter(dbCommand,"@rapcb", rights.rapcb);
      
        Database.PutParameter(dbCommand,"@rapcbt", rights.rapcbt);
      
        Database.PutParameter(dbCommand,"@rapcbe", rights.rapcbe);
      
        Database.PutParameter(dbCommand,"@rapcbo", rights.rapcbo);
      
        Database.PutParameter(dbCommand,"@rapcba", rights.rapcba);
      
        Database.PutParameter(dbCommand,"@rapcbp", rights.rapcbp);
      
        Database.PutParameter(dbCommand,"@rapcbg", rights.rapcbg);
      
        Database.PutParameter(dbCommand,"@rapcbj", rights.rapcbj);
      
        Database.PutParameter(dbCommand,"@rapcbr", rights.rapcbr);
      
        Database.PutParameter(dbCommand,"@rapcbrp", rights.rapcbrp);
      
        Database.PutParameter(dbCommand,"@rapcp", rights.rapcp);
      
        Database.PutParameter(dbCommand,"@rapcps", rights.rapcps);
      
        Database.PutParameter(dbCommand,"@rapcpc", rights.rapcpc);
      
        Database.PutParameter(dbCommand,"@rapcpr", rights.rapcpr);
      
        Database.PutParameter(dbCommand,"@rapcprp", rights.rapcprp);
      
        Database.PutParameter(dbCommand,"@rau", rights.rau);
      
        Database.PutParameter(dbCommand,"@raua", rights.raua);
      
        Database.PutParameter(dbCommand,"@rauc", rights.rauc);
      
        Database.PutParameter(dbCommand,"@raue", rights.raue);
      
        Database.PutParameter(dbCommand,"@rauh", rights.rauh);
      
        Database.PutParameter(dbCommand,"@raus", rights.raus);
      
        Database.PutParameter(dbCommand,"@raum", rights.raum);
      
        Database.PutParameter(dbCommand,"@rar", rights.rar);
      
        Database.PutParameter(dbCommand,"@rar1", rights.rar1);
      
        Database.PutParameter(dbCommand,"@rar2", rights.rar2);
      
        Database.PutParameter(dbCommand,"@rara", rights.rara);
      
        Database.PutParameter(dbCommand,"@rars", rights.rars);
      
        Database.PutParameter(dbCommand,"@rard", rights.rard);
      
        Database.PutParameter(dbCommand,"@rarh", rights.rarh);
      
        Database.PutParameter(dbCommand,"@rarl", rights.rarl);
      
        Database.PutParameter(dbCommand,"@rarm", rights.rarm);
      
        Database.PutParameter(dbCommand,"@rarn", rights.rarn);
      
        Database.PutParameter(dbCommand,"@raru", rights.raru);
      
        Database.PutParameter(dbCommand,"@rarg", rights.rarg);
      
        Database.PutParameter(dbCommand,"@rarc", rights.rarc);
      
        Database.PutParameter(dbCommand,"@rarr", rights.rarr);
      
        Database.PutParameter(dbCommand,"@rarb", rights.rarb);
      
        Database.PutParameter(dbCommand,"@rart", rights.rart);
      
        Database.PutParameter(dbCommand,"@rari", rights.rari);
      
        Database.PutParameter(dbCommand,"@rarz", rights.rarz);
      
        Database.PutParameter(dbCommand,"@rark", rights.rark);
      
        Database.PutParameter(dbCommand,"@rarw", rights.rarw);
      
        Database.PutParameter(dbCommand,"@raro", rights.raro);
      
        Database.PutParameter(dbCommand,"@rarp", rights.rarp);
      
        Database.PutParameter(dbCommand,"@ru", rights.ru);
      
        Database.PutParameter(dbCommand,"@rui", rights.rui);
      
        Database.PutParameter(dbCommand,"@ruo", rights.ruo);
      
        Database.PutParameter(dbCommand,"@rue", rights.rue);
      
        Database.PutParameter(dbCommand,"@ruq", rights.ruq);
      
        Database.PutParameter(dbCommand,"@ruw", rights.ruw);
      
        Database.PutParameter(dbCommand,"@ruc", rights.ruc);
      
        Database.PutParameter(dbCommand,"@rum", rights.rum);
      
        Database.PutParameter(dbCommand,"@rus", rights.rus);
      
        Database.PutParameter(dbCommand,"@rm", rights.rm);
      
        Database.PutParameter(dbCommand,"@rmr", rights.rmr);
      
        Database.PutParameter(dbCommand,"@rmra", rights.rmra);
      
        Database.PutParameter(dbCommand,"@rmrs", rights.rmrs);
      
        Database.PutParameter(dbCommand,"@rmrd", rights.rmrd);
      
        Database.PutParameter(dbCommand,"@rmrm", rights.rmrm);
      
        Database.PutParameter(dbCommand,"@rmrn", rights.rmrn);
      
        Database.PutParameter(dbCommand,"@rmrr", rights.rmrr);
      
        Database.PutParameter(dbCommand,"@rmrk", rights.rmrk);
      
        Database.PutParameter(dbCommand,"@rmrt", rights.rmrt);
      
        Database.PutParameter(dbCommand,"@rmrj", rights.rmrj);
      
        Database.PutParameter(dbCommand,"@rmrw", rights.rmrw);
      
        Database.PutParameter(dbCommand,"@rmrv", rights.rmrv);
      
        Database.PutParameter(dbCommand,"@rmre", rights.rmre);
      
        Database.PutParameter(dbCommand,"@rmrp", rights.rmrp);
      
        Database.PutParameter(dbCommand,"@rmro", rights.rmro);
      
        Database.PutParameter(dbCommand,"@rmrl", rights.rmrl);
      
        Database.PutParameter(dbCommand,"@rmry", rights.rmry);
      
        Database.PutParameter(dbCommand,"@rmrc", rights.rmrc);
      
        Database.PutParameter(dbCommand,"@rmrf", rights.rmrf);
      
        Database.PutParameter(dbCommand,"@rmg", rights.rmg);
      
        Database.PutParameter(dbCommand,"@rmp", rights.rmp);
      
        Database.PutParameter(dbCommand,"@rs", rights.rs);
      
        Database.PutParameter(dbCommand,"@rss", rights.rss);
      
        Database.PutParameter(dbCommand,"@rssa", rights.rssa);
      
        Database.PutParameter(dbCommand,"@rsse", rights.rsse);
      
        Database.PutParameter(dbCommand,"@rssr", rights.rssr);
      
        Database.PutParameter(dbCommand,"@rssu", rights.rssu);
      
        Database.PutParameter(dbCommand,"@rssz", rights.rssz);
      
        Database.PutParameter(dbCommand,"@rse", rights.rse);
      
        Database.PutParameter(dbCommand,"@rsu", rights.rsu);
      
        Database.PutParameter(dbCommand,"@rsc", rights.rsc);
      
        Database.PutParameter(dbCommand,"@rsi", rights.rsi);
      
        Database.PutParameter(dbCommand,"@rso", rights.rso);
      
        Database.PutParameter(dbCommand,"@rsl", rights.rsl);
      
        Database.PutParameter(dbCommand,"@rsv", rights.rsv);
      
        Database.PutParameter(dbCommand,"@rsd", rights.rsd);
      
        Database.PutParameter(dbCommand,"@rsea", rights.rsea);
      
        Database.PutParameter(dbCommand,"@rseb", rights.rseb);
      
        Database.PutParameter(dbCommand,"@rsec", rights.rsec);
      
        Database.PutParameter(dbCommand,"@rsed", rights.rsed);
      
        Database.PutParameter(dbCommand,"@rsee", rights.rsee);
      
        Database.PutParameter(dbCommand,"@rsef", rights.rsef);
      
        Database.PutParameter(dbCommand,"@rseg", rights.rseg);
      
        Database.PutParameter(dbCommand,"@rseh", rights.rseh);
      
        Database.PutParameter(dbCommand,"@rsei", rights.rsei);
      
        Database.PutParameter(dbCommand,"@rsem", rights.rsem);
      
        Database.PutParameter(dbCommand,"@rsen", rights.rsen);
      
        Database.PutParameter(dbCommand,"@rseo", rights.rseo);
      
        Database.PutParameter(dbCommand,"@rsep", rights.rsep);
      
        Database.PutParameter(dbCommand,"@rser", rights.rser);
      
        Database.PutParameter(dbCommand,"@rses", rights.rses);
      
        Database.PutParameter(dbCommand,"@rset", rights.rset);
      
        Database.PutParameter(dbCommand,"@rseu", rights.rseu);
      
        Database.PutParameter(dbCommand,"@rsev", rights.rsev);
      
        Database.PutParameter(dbCommand,"@rsex", rights.rsex);
      
        Database.PutParameter(dbCommand,"@rsey", rights.rsey);
      
        Database.PutParameter(dbCommand,"@rsel", rights.rsel);
      
        Database.PutParameter(dbCommand,"@rsez", rights.rsez);
      
        Database.PutParameter(dbCommand,"@rcell", rights.rcell);
      
        Database.PutParameter(dbCommand,"@rsysmenu", rights.rsysmenu);
      
        Database.PutParameter(dbCommand,"@sysadmin", rights.sysadmin);
      
        Database.PutParameter(dbCommand,"@old_id", rights.old_id);
      
        Database.PutParameter(dbCommand,"@savertime", rights.savertime);
      
        Database.PutParameter(dbCommand,"@localprt", rights.localprt);
      
        Database.PutParameter(dbCommand,"@rdc", rights.rdc);
      
        Database.PutParameter(dbCommand,"@ropr", rights.ropr);
      
        Database.PutParameter(dbCommand,"@rare", rights.rare);
      
        Database.PutParameter(dbCommand,"@rse1", rights.rse1);
      
        Database.PutParameter(dbCommand,"@compress", rights.compress);
      
        Database.PutParameter(dbCommand,"@rmru", rights.rmru);
      
        Database.PutParameter(dbCommand,"@rmrud", rights.rmrud);
      
        Database.PutParameter(dbCommand,"@rmrus", rights.rmrus);
      
        Database.PutParameter(dbCommand,"@rul", rights.rul);
      
        Database.PutParameter(dbCommand,"@rmrb", rights.rmrb);
      
        Database.PutParameter(dbCommand,"@rmrbt", rights.rmrbt);
      
        Database.PutParameter(dbCommand,"@rmrbc", rights.rmrbc);
      
        Database.PutParameter(dbCommand,"@rmrbd", rights.rmrbd);
      
        Database.PutParameter(dbCommand,"@rmrbcl", rights.rmrbcl);
      
        Database.PutParameter(dbCommand,"@rsh", rights.rsh);
      
        Database.PutParameter(dbCommand,"@pradmin", rights.pradmin);
      
        Database.PutParameter(dbCommand,"@unlimited", rights.unlimited);
      
        Database.PutParameter(dbCommand,"@rse2", rights.rse2);
      
        Database.PutParameter(dbCommand,"@rse3", rights.rse3);
      
        Database.PutParameter(dbCommand,"@rse4", rights.rse4);
      
        Database.PutParameter(dbCommand,"@rse5", rights.rse5);
      
        Database.PutParameter(dbCommand,"@rmr1", rights.rmr1);
      
        Database.PutParameter(dbCommand,"@rmi", rights.rmi);
      
        Database.PutParameter(dbCommand,"@rmim", rights.rmim);
      
        Database.PutParameter(dbCommand,"@rmir", rights.rmir);
      
        Database.PutParameter(dbCommand,"@rmis", rights.rmis);
      
        Database.PutParameter(dbCommand,"@rmird", rights.rmird);
      
        Database.PutParameter(dbCommand,"@rmirs", rights.rmirs);
      
        Database.PutParameter(dbCommand,"@ropl", rights.ropl);
      
        Database.PutParameter(dbCommand,"@ropt", rights.ropt);
      
        Database.PutParameter(dbCommand,"@rmr2", rights.rmr2);
      
        Database.PutParameter(dbCommand,"@rmr3", rights.rmr3);
      
        Database.PutParameter(dbCommand,"@rmrba", rights.rmrba);
      
        Database.PutParameter(dbCommand,"@roph", rights.roph);
      
        Database.PutParameter(dbCommand,"@rmr4", rights.rmr4);
      
        Database.PutParameter(dbCommand,"@rse6", rights.rse6);
      
        Database.PutParameter(dbCommand,"@company_id", rights.company_id);
      
        Database.PutParameter(dbCommand,"@dealer", rights.dealer);
      
        Database.PutParameter(dbCommand,"@userid", rights.userid);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " rights.userid, "
      
        + " rights.user, "
      
        + " rights.login_name, "
      
        + " rights.password, "
      
        + " rights.department, "
      
        + " rights.active, "
      
        + " rights.online, "
      
        + " rights.ro, "
      
        + " rights.roc, "
      
        + " rights.rom, "
      
        + " rights.ron, "
      
        + " rights.ror, "
      
        + " rights.row, "
      
        + " rights.rou, "
      
        + " rights.roua, "
      
        + " rights.rouk, "
      
        + " rights.roud, "
      
        + " rights.rouh, "
      
        + " rights.rouj, "
      
        + " rights.roul, "
      
        + " rights.rour, "
      
        + " rights.rous, "
      
        + " rights.rouc, "
      
        + " rights.roum, "
      
        + " rights.rout, "
      
        + " rights.roux, "
      
        + " rights.rop, "
      
        + " rights.ropa, "
      
        + " rights.ropf, "
      
        + " rights.ropd, "
      
        + " rights.ropc, "
      
        + " rights.rops, "
      
        + " rights.ropn, "
      
        + " rights.rd, "
      
        + " rights.rde, "
      
        + " rights.rdq, "
      
        + " rights.rdr, "
      
        + " rights.rds, "
      
        + " rights.rdu, "
      
        + " rights.rdua, "
      
        + " rights.rduc, "
      
        + " rights.rdud, "
      
        + " rights.rdup, "
      
        + " rights.rdus, "
      
        + " rights.rdp, "
      
        + " rights.rdpc, "
      
        + " rights.rdpd, "
      
        + " rights.rdpi, "
      
        + " rights.rdpl, "
      
        + " rights.rdpo, "
      
        + " rights.rdpp, "
      
        + " rights.rdps, "
      
        + " rights.rdpb, "
      
        + " rights.rdpn, "
      
        + " rights.rdpq, "
      
        + " rights.rdpu, "
      
        + " rights.rdpa, "
      
        + " rights.rdpaa, "
      
        + " rights.rdp1, "
      
        + " rights.rdj, "
      
        + " rights.ra, "
      
        + " rights.rac, "
      
        + " rights.rap, "
      
        + " rights.rapr, "
      
        + " rights.rapa, "
      
        + " rights.rapt, "
      
        + " rights.rapp, "
      
        + " rights.rapf, "
      
        + " rights.rapc, "
      
        + " rights.rapcb, "
      
        + " rights.rapcbt, "
      
        + " rights.rapcbe, "
      
        + " rights.rapcbo, "
      
        + " rights.rapcba, "
      
        + " rights.rapcbp, "
      
        + " rights.rapcbg, "
      
        + " rights.rapcbj, "
      
        + " rights.rapcbr, "
      
        + " rights.rapcbrp, "
      
        + " rights.rapcp, "
      
        + " rights.rapcps, "
      
        + " rights.rapcpc, "
      
        + " rights.rapcpr, "
      
        + " rights.rapcprp, "
      
        + " rights.rau, "
      
        + " rights.raua, "
      
        + " rights.rauc, "
      
        + " rights.raue, "
      
        + " rights.rauh, "
      
        + " rights.raus, "
      
        + " rights.raum, "
      
        + " rights.rar, "
      
        + " rights.rar1, "
      
        + " rights.rar2, "
      
        + " rights.rara, "
      
        + " rights.rars, "
      
        + " rights.rard, "
      
        + " rights.rarh, "
      
        + " rights.rarl, "
      
        + " rights.rarm, "
      
        + " rights.rarn, "
      
        + " rights.raru, "
      
        + " rights.rarg, "
      
        + " rights.rarc, "
      
        + " rights.rarr, "
      
        + " rights.rarb, "
      
        + " rights.rart, "
      
        + " rights.rari, "
      
        + " rights.rarz, "
      
        + " rights.rark, "
      
        + " rights.rarw, "
      
        + " rights.raro, "
      
        + " rights.rarp, "
      
        + " rights.ru, "
      
        + " rights.rui, "
      
        + " rights.ruo, "
      
        + " rights.rue, "
      
        + " rights.ruq, "
      
        + " rights.ruw, "
      
        + " rights.ruc, "
      
        + " rights.rum, "
      
        + " rights.rus, "
      
        + " rights.rm, "
      
        + " rights.rmr, "
      
        + " rights.rmra, "
      
        + " rights.rmrs, "
      
        + " rights.rmrd, "
      
        + " rights.rmrm, "
      
        + " rights.rmrn, "
      
        + " rights.rmrr, "
      
        + " rights.rmrk, "
      
        + " rights.rmrt, "
      
        + " rights.rmrj, "
      
        + " rights.rmrw, "
      
        + " rights.rmrv, "
      
        + " rights.rmre, "
      
        + " rights.rmrp, "
      
        + " rights.rmro, "
      
        + " rights.rmrl, "
      
        + " rights.rmry, "
      
        + " rights.rmrc, "
      
        + " rights.rmrf, "
      
        + " rights.rmg, "
      
        + " rights.rmp, "
      
        + " rights.rs, "
      
        + " rights.rss, "
      
        + " rights.rssa, "
      
        + " rights.rsse, "
      
        + " rights.rssr, "
      
        + " rights.rssu, "
      
        + " rights.rssz, "
      
        + " rights.rse, "
      
        + " rights.rsu, "
      
        + " rights.rsc, "
      
        + " rights.rsi, "
      
        + " rights.rso, "
      
        + " rights.rsl, "
      
        + " rights.rsv, "
      
        + " rights.rsd, "
      
        + " rights.rsea, "
      
        + " rights.rseb, "
      
        + " rights.rsec, "
      
        + " rights.rsed, "
      
        + " rights.rsee, "
      
        + " rights.rsef, "
      
        + " rights.rseg, "
      
        + " rights.rseh, "
      
        + " rights.rsei, "
      
        + " rights.rsem, "
      
        + " rights.rsen, "
      
        + " rights.rseo, "
      
        + " rights.rsep, "
      
        + " rights.rser, "
      
        + " rights.rses, "
      
        + " rights.rset, "
      
        + " rights.rseu, "
      
        + " rights.rsev, "
      
        + " rights.rsex, "
      
        + " rights.rsey, "
      
        + " rights.rsel, "
      
        + " rights.rsez, "
      
        + " rights.rcell, "
      
        + " rights.rsysmenu, "
      
        + " rights.sysadmin, "
      
        + " rights.old_id, "
      
        + " rights.savertime, "
      
        + " rights.localprt, "
      
        + " rights.rdc, "
      
        + " rights.ropr, "
      
        + " rights.rare, "
      
        + " rights.rse1, "
      
        + " rights.compress, "
      
        + " rights.rmru, "
      
        + " rights.rmrud, "
      
        + " rights.rmrus, "
      
        + " rights.rul, "
      
        + " rights.rmrb, "
      
        + " rights.rmrbt, "
      
        + " rights.rmrbc, "
      
        + " rights.rmrbd, "
      
        + " rights.rmrbcl, "
      
        + " rights.rsh, "
      
        + " rights.pradmin, "
      
        + " rights.unlimited, "
      
        + " rights.rse2, "
      
        + " rights.rse3, "
      
        + " rights.rse4, "
      
        + " rights.rse5, "
      
        + " rights.rmr1, "
      
        + " rights.rmi, "
      
        + " rights.rmim, "
      
        + " rights.rmir, "
      
        + " rights.rmis, "
      
        + " rights.rmird, "
      
        + " rights.rmirs, "
      
        + " rights.ropl, "
      
        + " rights.ropt, "
      
        + " rights.rmr2, "
      
        + " rights.rmr3, "
      
        + " rights.rmrba, "
      
        + " rights.roph, "
      
        + " rights.rmr4, "
      
        + " rights.rse6, "
      
        + " rights.company_id, "
      
        + " rights.dealer "
      

      + " From rights "

      
        + " Where "
        
          + " rights.userid = ?  "
        
      ;

      public static rights FindByPrimaryKey(
      String userid
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@userid", userid);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("rights not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(rights rights)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@userid",rights.userid);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from rights";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, ConnectionKeyEnum.Servman))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static rights Load(IDataReader dataReader)
      {
      rights rights = new rights();

      rights.userid = dataReader.GetString(0);
          rights.user = dataReader.GetString(1);
          rights.login_name = dataReader.GetString(2);
          rights.password = dataReader.GetString(3);
          rights.department = dataReader.GetInt32(4);
          rights.active = dataReader.GetBoolean(5);
          rights.online = dataReader.GetBoolean(6);
          rights.ro = dataReader.GetBoolean(7);
          rights.roc = dataReader.GetBoolean(8);
          rights.rom = dataReader.GetBoolean(9);
          rights.ron = dataReader.GetBoolean(10);
          rights.ror = dataReader.GetBoolean(11);
          rights.row = dataReader.GetBoolean(12);
          rights.rou = dataReader.GetBoolean(13);
          rights.roua = dataReader.GetBoolean(14);
          rights.rouk = dataReader.GetBoolean(15);
          rights.roud = dataReader.GetBoolean(16);
          rights.rouh = dataReader.GetBoolean(17);
          rights.rouj = dataReader.GetBoolean(18);
          rights.roul = dataReader.GetBoolean(19);
          rights.rour = dataReader.GetBoolean(20);
          rights.rous = dataReader.GetBoolean(21);
          rights.rouc = dataReader.GetBoolean(22);
          rights.roum = dataReader.GetBoolean(23);
          rights.rout = dataReader.GetBoolean(24);
          rights.roux = dataReader.GetBoolean(25);
          rights.rop = dataReader.GetBoolean(26);
          rights.ropa = dataReader.GetBoolean(27);
          rights.ropf = dataReader.GetBoolean(28);
          rights.ropd = dataReader.GetBoolean(29);
          rights.ropc = dataReader.GetBoolean(30);
          rights.rops = dataReader.GetBoolean(31);
          rights.ropn = dataReader.GetBoolean(32);
          rights.rd = dataReader.GetBoolean(33);
          rights.rde = dataReader.GetBoolean(34);
          rights.rdq = dataReader.GetBoolean(35);
          rights.rdr = dataReader.GetBoolean(36);
          rights.rds = dataReader.GetBoolean(37);
          rights.rdu = dataReader.GetBoolean(38);
          rights.rdua = dataReader.GetBoolean(39);
          rights.rduc = dataReader.GetBoolean(40);
          rights.rdud = dataReader.GetBoolean(41);
          rights.rdup = dataReader.GetBoolean(42);
          rights.rdus = dataReader.GetBoolean(43);
          rights.rdp = dataReader.GetBoolean(44);
          rights.rdpc = dataReader.GetBoolean(45);
          rights.rdpd = dataReader.GetBoolean(46);
          rights.rdpi = dataReader.GetBoolean(47);
          rights.rdpl = dataReader.GetBoolean(48);
          rights.rdpo = dataReader.GetBoolean(49);
          rights.rdpp = dataReader.GetBoolean(50);
          rights.rdps = dataReader.GetBoolean(51);
          rights.rdpb = dataReader.GetBoolean(52);
          rights.rdpn = dataReader.GetBoolean(53);
          rights.rdpq = dataReader.GetBoolean(54);
          rights.rdpu = dataReader.GetBoolean(55);
          rights.rdpa = dataReader.GetBoolean(56);
          rights.rdpaa = dataReader.GetBoolean(57);
          rights.rdp1 = dataReader.GetBoolean(58);
          rights.rdj = dataReader.GetBoolean(59);
          rights.ra = dataReader.GetBoolean(60);
          rights.rac = dataReader.GetBoolean(61);
          rights.rap = dataReader.GetBoolean(62);
          rights.rapr = dataReader.GetBoolean(63);
          rights.rapa = dataReader.GetBoolean(64);
          rights.rapt = dataReader.GetBoolean(65);
          rights.rapp = dataReader.GetBoolean(66);
          rights.rapf = dataReader.GetBoolean(67);
          rights.rapc = dataReader.GetBoolean(68);
          rights.rapcb = dataReader.GetBoolean(69);
          rights.rapcbt = dataReader.GetBoolean(70);
          rights.rapcbe = dataReader.GetBoolean(71);
          rights.rapcbo = dataReader.GetBoolean(72);
          rights.rapcba = dataReader.GetBoolean(73);
          rights.rapcbp = dataReader.GetBoolean(74);
          rights.rapcbg = dataReader.GetBoolean(75);
          rights.rapcbj = dataReader.GetBoolean(76);
          rights.rapcbr = dataReader.GetBoolean(77);
          rights.rapcbrp = dataReader.GetBoolean(78);
          rights.rapcp = dataReader.GetBoolean(79);
          rights.rapcps = dataReader.GetBoolean(80);
          rights.rapcpc = dataReader.GetBoolean(81);
          rights.rapcpr = dataReader.GetBoolean(82);
          rights.rapcprp = dataReader.GetBoolean(83);
          rights.rau = dataReader.GetBoolean(84);
          rights.raua = dataReader.GetBoolean(85);
          rights.rauc = dataReader.GetBoolean(86);
          rights.raue = dataReader.GetBoolean(87);
          rights.rauh = dataReader.GetBoolean(88);
          rights.raus = dataReader.GetBoolean(89);
          rights.raum = dataReader.GetBoolean(90);
          rights.rar = dataReader.GetBoolean(91);
          rights.rar1 = dataReader.GetBoolean(92);
          rights.rar2 = dataReader.GetBoolean(93);
          rights.rara = dataReader.GetBoolean(94);
          rights.rars = dataReader.GetBoolean(95);
          rights.rard = dataReader.GetBoolean(96);
          rights.rarh = dataReader.GetBoolean(97);
          rights.rarl = dataReader.GetBoolean(98);
          rights.rarm = dataReader.GetBoolean(99);
          rights.rarn = dataReader.GetBoolean(100);
          rights.raru = dataReader.GetBoolean(101);
          rights.rarg = dataReader.GetBoolean(102);
          rights.rarc = dataReader.GetBoolean(103);
          rights.rarr = dataReader.GetBoolean(104);
          rights.rarb = dataReader.GetBoolean(105);
          rights.rart = dataReader.GetBoolean(106);
          rights.rari = dataReader.GetBoolean(107);
          rights.rarz = dataReader.GetBoolean(108);
          rights.rark = dataReader.GetBoolean(109);
          rights.rarw = dataReader.GetBoolean(110);
          rights.raro = dataReader.GetBoolean(111);
          rights.rarp = dataReader.GetBoolean(112);
          rights.ru = dataReader.GetBoolean(113);
          rights.rui = dataReader.GetBoolean(114);
          rights.ruo = dataReader.GetBoolean(115);
          rights.rue = dataReader.GetBoolean(116);
          rights.ruq = dataReader.GetBoolean(117);
          rights.ruw = dataReader.GetBoolean(118);
          rights.ruc = dataReader.GetBoolean(119);
          rights.rum = dataReader.GetBoolean(120);
          rights.rus = dataReader.GetBoolean(121);
          rights.rm = dataReader.GetBoolean(122);
          rights.rmr = dataReader.GetBoolean(123);
          rights.rmra = dataReader.GetBoolean(124);
          rights.rmrs = dataReader.GetBoolean(125);
          rights.rmrd = dataReader.GetBoolean(126);
          rights.rmrm = dataReader.GetBoolean(127);
          rights.rmrn = dataReader.GetBoolean(128);
          rights.rmrr = dataReader.GetBoolean(129);
          rights.rmrk = dataReader.GetBoolean(130);
          rights.rmrt = dataReader.GetBoolean(131);
          rights.rmrj = dataReader.GetBoolean(132);
          rights.rmrw = dataReader.GetBoolean(133);
          rights.rmrv = dataReader.GetBoolean(134);
          rights.rmre = dataReader.GetBoolean(135);
          rights.rmrp = dataReader.GetBoolean(136);
          rights.rmro = dataReader.GetBoolean(137);
          rights.rmrl = dataReader.GetBoolean(138);
          rights.rmry = dataReader.GetBoolean(139);
          rights.rmrc = dataReader.GetBoolean(140);
          rights.rmrf = dataReader.GetBoolean(141);
          rights.rmg = dataReader.GetBoolean(142);
          rights.rmp = dataReader.GetBoolean(143);
          rights.rs = dataReader.GetBoolean(144);
          rights.rss = dataReader.GetBoolean(145);
          rights.rssa = dataReader.GetBoolean(146);
          rights.rsse = dataReader.GetBoolean(147);
          rights.rssr = dataReader.GetBoolean(148);
          rights.rssu = dataReader.GetBoolean(149);
          rights.rssz = dataReader.GetBoolean(150);
          rights.rse = dataReader.GetBoolean(151);
          rights.rsu = dataReader.GetBoolean(152);
          rights.rsc = dataReader.GetBoolean(153);
          rights.rsi = dataReader.GetBoolean(154);
          rights.rso = dataReader.GetBoolean(155);
          rights.rsl = dataReader.GetBoolean(156);
          rights.rsv = dataReader.GetBoolean(157);
          rights.rsd = dataReader.GetBoolean(158);
          rights.rsea = dataReader.GetBoolean(159);
          rights.rseb = dataReader.GetBoolean(160);
          rights.rsec = dataReader.GetBoolean(161);
          rights.rsed = dataReader.GetBoolean(162);
          rights.rsee = dataReader.GetBoolean(163);
          rights.rsef = dataReader.GetBoolean(164);
          rights.rseg = dataReader.GetBoolean(165);
          rights.rseh = dataReader.GetBoolean(166);
          rights.rsei = dataReader.GetBoolean(167);
          rights.rsem = dataReader.GetBoolean(168);
          rights.rsen = dataReader.GetBoolean(169);
          rights.rseo = dataReader.GetBoolean(170);
          rights.rsep = dataReader.GetBoolean(171);
          rights.rser = dataReader.GetBoolean(172);
          rights.rses = dataReader.GetBoolean(173);
          rights.rset = dataReader.GetBoolean(174);
          rights.rseu = dataReader.GetBoolean(175);
          rights.rsev = dataReader.GetBoolean(176);
          rights.rsex = dataReader.GetBoolean(177);
          rights.rsey = dataReader.GetBoolean(178);
          rights.rsel = dataReader.GetBoolean(179);
          rights.rsez = dataReader.GetBoolean(180);
          rights.rcell = dataReader.GetBoolean(181);
          rights.rsysmenu = dataReader.GetBoolean(182);
          rights.sysadmin = dataReader.GetBoolean(183);
          rights.old_id = dataReader.GetString(184);
          rights.savertime = dataReader.GetInt32(185);
          rights.localprt = dataReader.GetBoolean(186);
          rights.rdc = dataReader.GetBoolean(187);
          rights.ropr = dataReader.GetBoolean(188);
          rights.rare = dataReader.GetBoolean(189);
          rights.rse1 = dataReader.GetBoolean(190);
          rights.compress = dataReader.GetString(191);
          rights.rmru = dataReader.GetBoolean(192);
          rights.rmrud = dataReader.GetBoolean(193);
          rights.rmrus = dataReader.GetBoolean(194);
          rights.rul = dataReader.GetBoolean(195);
          rights.rmrb = dataReader.GetBoolean(196);
          rights.rmrbt = dataReader.GetBoolean(197);
          rights.rmrbc = dataReader.GetBoolean(198);
          rights.rmrbd = dataReader.GetBoolean(199);
          rights.rmrbcl = dataReader.GetBoolean(200);
          rights.rsh = dataReader.GetBoolean(201);
          rights.pradmin = dataReader.GetBoolean(202);
          rights.unlimited = dataReader.GetBoolean(203);
          rights.rse2 = dataReader.GetBoolean(204);
          rights.rse3 = dataReader.GetBoolean(205);
          rights.rse4 = dataReader.GetBoolean(206);
          rights.rse5 = dataReader.GetBoolean(207);
          rights.rmr1 = dataReader.GetBoolean(208);
          rights.rmi = dataReader.GetBoolean(209);
          rights.rmim = dataReader.GetBoolean(210);
          rights.rmir = dataReader.GetBoolean(211);
          rights.rmis = dataReader.GetBoolean(212);
          rights.rmird = dataReader.GetBoolean(213);
          rights.rmirs = dataReader.GetBoolean(214);
          rights.ropl = dataReader.GetBoolean(215);
          rights.ropt = dataReader.GetBoolean(216);
          rights.rmr2 = dataReader.GetBoolean(217);
          rights.rmr3 = dataReader.GetBoolean(218);
          rights.rmrba = dataReader.GetBoolean(219);
          rights.roph = dataReader.GetBoolean(220);
          rights.rmr4 = dataReader.GetBoolean(221);
          rights.rse6 = dataReader.GetBoolean(222);
          rights.company_id = dataReader.GetInt32(223);
          rights.dealer = dataReader.GetBoolean(224);
          

      return rights;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [rights] "

      
        + " Where "
        
          + " userid = ?  "
        
      ;
      public static void Delete(rights rights)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@userid", rights.userid);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [rights] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, ConnectionKeyEnum.Servman))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " rights.userid, "
      
        + " rights.user, "
      
        + " rights.login_name, "
      
        + " rights.password, "
      
        + " rights.department, "
      
        + " rights.active, "
      
        + " rights.online, "
      
        + " rights.ro, "
      
        + " rights.roc, "
      
        + " rights.rom, "
      
        + " rights.ron, "
      
        + " rights.ror, "
      
        + " rights.row, "
      
        + " rights.rou, "
      
        + " rights.roua, "
      
        + " rights.rouk, "
      
        + " rights.roud, "
      
        + " rights.rouh, "
      
        + " rights.rouj, "
      
        + " rights.roul, "
      
        + " rights.rour, "
      
        + " rights.rous, "
      
        + " rights.rouc, "
      
        + " rights.roum, "
      
        + " rights.rout, "
      
        + " rights.roux, "
      
        + " rights.rop, "
      
        + " rights.ropa, "
      
        + " rights.ropf, "
      
        + " rights.ropd, "
      
        + " rights.ropc, "
      
        + " rights.rops, "
      
        + " rights.ropn, "
      
        + " rights.rd, "
      
        + " rights.rde, "
      
        + " rights.rdq, "
      
        + " rights.rdr, "
      
        + " rights.rds, "
      
        + " rights.rdu, "
      
        + " rights.rdua, "
      
        + " rights.rduc, "
      
        + " rights.rdud, "
      
        + " rights.rdup, "
      
        + " rights.rdus, "
      
        + " rights.rdp, "
      
        + " rights.rdpc, "
      
        + " rights.rdpd, "
      
        + " rights.rdpi, "
      
        + " rights.rdpl, "
      
        + " rights.rdpo, "
      
        + " rights.rdpp, "
      
        + " rights.rdps, "
      
        + " rights.rdpb, "
      
        + " rights.rdpn, "
      
        + " rights.rdpq, "
      
        + " rights.rdpu, "
      
        + " rights.rdpa, "
      
        + " rights.rdpaa, "
      
        + " rights.rdp1, "
      
        + " rights.rdj, "
      
        + " rights.ra, "
      
        + " rights.rac, "
      
        + " rights.rap, "
      
        + " rights.rapr, "
      
        + " rights.rapa, "
      
        + " rights.rapt, "
      
        + " rights.rapp, "
      
        + " rights.rapf, "
      
        + " rights.rapc, "
      
        + " rights.rapcb, "
      
        + " rights.rapcbt, "
      
        + " rights.rapcbe, "
      
        + " rights.rapcbo, "
      
        + " rights.rapcba, "
      
        + " rights.rapcbp, "
      
        + " rights.rapcbg, "
      
        + " rights.rapcbj, "
      
        + " rights.rapcbr, "
      
        + " rights.rapcbrp, "
      
        + " rights.rapcp, "
      
        + " rights.rapcps, "
      
        + " rights.rapcpc, "
      
        + " rights.rapcpr, "
      
        + " rights.rapcprp, "
      
        + " rights.rau, "
      
        + " rights.raua, "
      
        + " rights.rauc, "
      
        + " rights.raue, "
      
        + " rights.rauh, "
      
        + " rights.raus, "
      
        + " rights.raum, "
      
        + " rights.rar, "
      
        + " rights.rar1, "
      
        + " rights.rar2, "
      
        + " rights.rara, "
      
        + " rights.rars, "
      
        + " rights.rard, "
      
        + " rights.rarh, "
      
        + " rights.rarl, "
      
        + " rights.rarm, "
      
        + " rights.rarn, "
      
        + " rights.raru, "
      
        + " rights.rarg, "
      
        + " rights.rarc, "
      
        + " rights.rarr, "
      
        + " rights.rarb, "
      
        + " rights.rart, "
      
        + " rights.rari, "
      
        + " rights.rarz, "
      
        + " rights.rark, "
      
        + " rights.rarw, "
      
        + " rights.raro, "
      
        + " rights.rarp, "
      
        + " rights.ru, "
      
        + " rights.rui, "
      
        + " rights.ruo, "
      
        + " rights.rue, "
      
        + " rights.ruq, "
      
        + " rights.ruw, "
      
        + " rights.ruc, "
      
        + " rights.rum, "
      
        + " rights.rus, "
      
        + " rights.rm, "
      
        + " rights.rmr, "
      
        + " rights.rmra, "
      
        + " rights.rmrs, "
      
        + " rights.rmrd, "
      
        + " rights.rmrm, "
      
        + " rights.rmrn, "
      
        + " rights.rmrr, "
      
        + " rights.rmrk, "
      
        + " rights.rmrt, "
      
        + " rights.rmrj, "
      
        + " rights.rmrw, "
      
        + " rights.rmrv, "
      
        + " rights.rmre, "
      
        + " rights.rmrp, "
      
        + " rights.rmro, "
      
        + " rights.rmrl, "
      
        + " rights.rmry, "
      
        + " rights.rmrc, "
      
        + " rights.rmrf, "
      
        + " rights.rmg, "
      
        + " rights.rmp, "
      
        + " rights.rs, "
      
        + " rights.rss, "
      
        + " rights.rssa, "
      
        + " rights.rsse, "
      
        + " rights.rssr, "
      
        + " rights.rssu, "
      
        + " rights.rssz, "
      
        + " rights.rse, "
      
        + " rights.rsu, "
      
        + " rights.rsc, "
      
        + " rights.rsi, "
      
        + " rights.rso, "
      
        + " rights.rsl, "
      
        + " rights.rsv, "
      
        + " rights.rsd, "
      
        + " rights.rsea, "
      
        + " rights.rseb, "
      
        + " rights.rsec, "
      
        + " rights.rsed, "
      
        + " rights.rsee, "
      
        + " rights.rsef, "
      
        + " rights.rseg, "
      
        + " rights.rseh, "
      
        + " rights.rsei, "
      
        + " rights.rsem, "
      
        + " rights.rsen, "
      
        + " rights.rseo, "
      
        + " rights.rsep, "
      
        + " rights.rser, "
      
        + " rights.rses, "
      
        + " rights.rset, "
      
        + " rights.rseu, "
      
        + " rights.rsev, "
      
        + " rights.rsex, "
      
        + " rights.rsey, "
      
        + " rights.rsel, "
      
        + " rights.rsez, "
      
        + " rights.rcell, "
      
        + " rights.rsysmenu, "
      
        + " rights.sysadmin, "
      
        + " rights.old_id, "
      
        + " rights.savertime, "
      
        + " rights.localprt, "
      
        + " rights.rdc, "
      
        + " rights.ropr, "
      
        + " rights.rare, "
      
        + " rights.rse1, "
      
        + " rights.compress, "
      
        + " rights.rmru, "
      
        + " rights.rmrud, "
      
        + " rights.rmrus, "
      
        + " rights.rul, "
      
        + " rights.rmrb, "
      
        + " rights.rmrbt, "
      
        + " rights.rmrbc, "
      
        + " rights.rmrbd, "
      
        + " rights.rmrbcl, "
      
        + " rights.rsh, "
      
        + " rights.pradmin, "
      
        + " rights.unlimited, "
      
        + " rights.rse2, "
      
        + " rights.rse3, "
      
        + " rights.rse4, "
      
        + " rights.rse5, "
      
        + " rights.rmr1, "
      
        + " rights.rmi, "
      
        + " rights.rmim, "
      
        + " rights.rmir, "
      
        + " rights.rmis, "
      
        + " rights.rmird, "
      
        + " rights.rmirs, "
      
        + " rights.ropl, "
      
        + " rights.ropt, "
      
        + " rights.rmr2, "
      
        + " rights.rmr3, "
      
        + " rights.rmrba, "
      
        + " rights.roph, "
      
        + " rights.rmr4, "
      
        + " rights.rse6, "
      
        + " rights.company_id, "
      
        + " rights.dealer "
      

      + " From rights ";
      public static List<rights> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<rights> rv = new List<rights>();

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      while(dataReader.Read())
      {
      rv.Add(Load(dataReader));
      }

      }

      return rv;
      }

      }
      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<rights> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<rights> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(rights));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(rights item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<rights>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(rights));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<rights> itemsList
      = new List<rights>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is rights)
      itemsList.Add(deserializedObject as rights);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_userid;
      
        protected String m_user;
      
        protected String m_login_name;
      
        protected String m_password;
      
        protected int m_department;
      
        protected bool m_active;
      
        protected bool m_online;
      
        protected bool m_ro;
      
        protected bool m_roc;
      
        protected bool m_rom;
      
        protected bool m_ron;
      
        protected bool m_ror;
      
        protected bool m_row;
      
        protected bool m_rou;
      
        protected bool m_roua;
      
        protected bool m_rouk;
      
        protected bool m_roud;
      
        protected bool m_rouh;
      
        protected bool m_rouj;
      
        protected bool m_roul;
      
        protected bool m_rour;
      
        protected bool m_rous;
      
        protected bool m_rouc;
      
        protected bool m_roum;
      
        protected bool m_rout;
      
        protected bool m_roux;
      
        protected bool m_rop;
      
        protected bool m_ropa;
      
        protected bool m_ropf;
      
        protected bool m_ropd;
      
        protected bool m_ropc;
      
        protected bool m_rops;
      
        protected bool m_ropn;
      
        protected bool m_rd;
      
        protected bool m_rde;
      
        protected bool m_rdq;
      
        protected bool m_rdr;
      
        protected bool m_rds;
      
        protected bool m_rdu;
      
        protected bool m_rdua;
      
        protected bool m_rduc;
      
        protected bool m_rdud;
      
        protected bool m_rdup;
      
        protected bool m_rdus;
      
        protected bool m_rdp;
      
        protected bool m_rdpc;
      
        protected bool m_rdpd;
      
        protected bool m_rdpi;
      
        protected bool m_rdpl;
      
        protected bool m_rdpo;
      
        protected bool m_rdpp;
      
        protected bool m_rdps;
      
        protected bool m_rdpb;
      
        protected bool m_rdpn;
      
        protected bool m_rdpq;
      
        protected bool m_rdpu;
      
        protected bool m_rdpa;
      
        protected bool m_rdpaa;
      
        protected bool m_rdp1;
      
        protected bool m_rdj;
      
        protected bool m_ra;
      
        protected bool m_rac;
      
        protected bool m_rap;
      
        protected bool m_rapr;
      
        protected bool m_rapa;
      
        protected bool m_rapt;
      
        protected bool m_rapp;
      
        protected bool m_rapf;
      
        protected bool m_rapc;
      
        protected bool m_rapcb;
      
        protected bool m_rapcbt;
      
        protected bool m_rapcbe;
      
        protected bool m_rapcbo;
      
        protected bool m_rapcba;
      
        protected bool m_rapcbp;
      
        protected bool m_rapcbg;
      
        protected bool m_rapcbj;
      
        protected bool m_rapcbr;
      
        protected bool m_rapcbrp;
      
        protected bool m_rapcp;
      
        protected bool m_rapcps;
      
        protected bool m_rapcpc;
      
        protected bool m_rapcpr;
      
        protected bool m_rapcprp;
      
        protected bool m_rau;
      
        protected bool m_raua;
      
        protected bool m_rauc;
      
        protected bool m_raue;
      
        protected bool m_rauh;
      
        protected bool m_raus;
      
        protected bool m_raum;
      
        protected bool m_rar;
      
        protected bool m_rar1;
      
        protected bool m_rar2;
      
        protected bool m_rara;
      
        protected bool m_rars;
      
        protected bool m_rard;
      
        protected bool m_rarh;
      
        protected bool m_rarl;
      
        protected bool m_rarm;
      
        protected bool m_rarn;
      
        protected bool m_raru;
      
        protected bool m_rarg;
      
        protected bool m_rarc;
      
        protected bool m_rarr;
      
        protected bool m_rarb;
      
        protected bool m_rart;
      
        protected bool m_rari;
      
        protected bool m_rarz;
      
        protected bool m_rark;
      
        protected bool m_rarw;
      
        protected bool m_raro;
      
        protected bool m_rarp;
      
        protected bool m_ru;
      
        protected bool m_rui;
      
        protected bool m_ruo;
      
        protected bool m_rue;
      
        protected bool m_ruq;
      
        protected bool m_ruw;
      
        protected bool m_ruc;
      
        protected bool m_rum;
      
        protected bool m_rus;
      
        protected bool m_rm;
      
        protected bool m_rmr;
      
        protected bool m_rmra;
      
        protected bool m_rmrs;
      
        protected bool m_rmrd;
      
        protected bool m_rmrm;
      
        protected bool m_rmrn;
      
        protected bool m_rmrr;
      
        protected bool m_rmrk;
      
        protected bool m_rmrt;
      
        protected bool m_rmrj;
      
        protected bool m_rmrw;
      
        protected bool m_rmrv;
      
        protected bool m_rmre;
      
        protected bool m_rmrp;
      
        protected bool m_rmro;
      
        protected bool m_rmrl;
      
        protected bool m_rmry;
      
        protected bool m_rmrc;
      
        protected bool m_rmrf;
      
        protected bool m_rmg;
      
        protected bool m_rmp;
      
        protected bool m_rs;
      
        protected bool m_rss;
      
        protected bool m_rssa;
      
        protected bool m_rsse;
      
        protected bool m_rssr;
      
        protected bool m_rssu;
      
        protected bool m_rssz;
      
        protected bool m_rse;
      
        protected bool m_rsu;
      
        protected bool m_rsc;
      
        protected bool m_rsi;
      
        protected bool m_rso;
      
        protected bool m_rsl;
      
        protected bool m_rsv;
      
        protected bool m_rsd;
      
        protected bool m_rsea;
      
        protected bool m_rseb;
      
        protected bool m_rsec;
      
        protected bool m_rsed;
      
        protected bool m_rsee;
      
        protected bool m_rsef;
      
        protected bool m_rseg;
      
        protected bool m_rseh;
      
        protected bool m_rsei;
      
        protected bool m_rsem;
      
        protected bool m_rsen;
      
        protected bool m_rseo;
      
        protected bool m_rsep;
      
        protected bool m_rser;
      
        protected bool m_rses;
      
        protected bool m_rset;
      
        protected bool m_rseu;
      
        protected bool m_rsev;
      
        protected bool m_rsex;
      
        protected bool m_rsey;
      
        protected bool m_rsel;
      
        protected bool m_rsez;
      
        protected bool m_rcell;
      
        protected bool m_rsysmenu;
      
        protected bool m_sysadmin;
      
        protected String m_old_id;
      
        protected int m_savertime;
      
        protected bool m_localprt;
      
        protected bool m_rdc;
      
        protected bool m_ropr;
      
        protected bool m_rare;
      
        protected bool m_rse1;
      
        protected String m_compress;
      
        protected bool m_rmru;
      
        protected bool m_rmrud;
      
        protected bool m_rmrus;
      
        protected bool m_rul;
      
        protected bool m_rmrb;
      
        protected bool m_rmrbt;
      
        protected bool m_rmrbc;
      
        protected bool m_rmrbd;
      
        protected bool m_rmrbcl;
      
        protected bool m_rsh;
      
        protected bool m_pradmin;
      
        protected bool m_unlimited;
      
        protected bool m_rse2;
      
        protected bool m_rse3;
      
        protected bool m_rse4;
      
        protected bool m_rse5;
      
        protected bool m_rmr1;
      
        protected bool m_rmi;
      
        protected bool m_rmim;
      
        protected bool m_rmir;
      
        protected bool m_rmis;
      
        protected bool m_rmird;
      
        protected bool m_rmirs;
      
        protected bool m_ropl;
      
        protected bool m_ropt;
      
        protected bool m_rmr2;
      
        protected bool m_rmr3;
      
        protected bool m_rmrba;
      
        protected bool m_roph;
      
        protected bool m_rmr4;
      
        protected bool m_rse6;
      
        protected int m_company_id;
      
        protected bool m_dealer;
      
      #endregion

      #region Constructors
      public rights(
      String 
          userid
      )
      {
      
        m_userid = userid;
      
      }

      


        public rights(
        String 
          userid,String 
          user,String 
          login_name,String 
          password,int 
          department,bool 
          active,bool 
          online,bool 
          ro,bool 
          roc,bool 
          rom,bool 
          ron,bool 
          ror,bool 
          row,bool 
          rou,bool 
          roua,bool 
          rouk,bool 
          roud,bool 
          rouh,bool 
          rouj,bool 
          roul,bool 
          rour,bool 
          rous,bool 
          rouc,bool 
          roum,bool 
          rout,bool 
          roux,bool 
          rop,bool 
          ropa,bool 
          ropf,bool 
          ropd,bool 
          ropc,bool 
          rops,bool 
          ropn,bool 
          rd,bool 
          rde,bool 
          rdq,bool 
          rdr,bool 
          rds,bool 
          rdu,bool 
          rdua,bool 
          rduc,bool 
          rdud,bool 
          rdup,bool 
          rdus,bool 
          rdp,bool 
          rdpc,bool 
          rdpd,bool 
          rdpi,bool 
          rdpl,bool 
          rdpo,bool 
          rdpp,bool 
          rdps,bool 
          rdpb,bool 
          rdpn,bool 
          rdpq,bool 
          rdpu,bool 
          rdpa,bool 
          rdpaa,bool 
          rdp1,bool 
          rdj,bool 
          ra,bool 
          rac,bool 
          rap,bool 
          rapr,bool 
          rapa,bool 
          rapt,bool 
          rapp,bool 
          rapf,bool 
          rapc,bool 
          rapcb,bool 
          rapcbt,bool 
          rapcbe,bool 
          rapcbo,bool 
          rapcba,bool 
          rapcbp,bool 
          rapcbg,bool 
          rapcbj,bool 
          rapcbr,bool 
          rapcbrp,bool 
          rapcp,bool 
          rapcps,bool 
          rapcpc,bool 
          rapcpr,bool 
          rapcprp,bool 
          rau,bool 
          raua,bool 
          rauc,bool 
          raue,bool 
          rauh,bool 
          raus,bool 
          raum,bool 
          rar,bool 
          rar1,bool 
          rar2,bool 
          rara,bool 
          rars,bool 
          rard,bool 
          rarh,bool 
          rarl,bool 
          rarm,bool 
          rarn,bool 
          raru,bool 
          rarg,bool 
          rarc,bool 
          rarr,bool 
          rarb,bool 
          rart,bool 
          rari,bool 
          rarz,bool 
          rark,bool 
          rarw,bool 
          raro,bool 
          rarp,bool 
          ru,bool 
          rui,bool 
          ruo,bool 
          rue,bool 
          ruq,bool 
          ruw,bool 
          ruc,bool 
          rum,bool 
          rus,bool 
          rm,bool 
          rmr,bool 
          rmra,bool 
          rmrs,bool 
          rmrd,bool 
          rmrm,bool 
          rmrn,bool 
          rmrr,bool 
          rmrk,bool 
          rmrt,bool 
          rmrj,bool 
          rmrw,bool 
          rmrv,bool 
          rmre,bool 
          rmrp,bool 
          rmro,bool 
          rmrl,bool 
          rmry,bool 
          rmrc,bool 
          rmrf,bool 
          rmg,bool 
          rmp,bool 
          rs,bool 
          rss,bool 
          rssa,bool 
          rsse,bool 
          rssr,bool 
          rssu,bool 
          rssz,bool 
          rse,bool 
          rsu,bool 
          rsc,bool 
          rsi,bool 
          rso,bool 
          rsl,bool 
          rsv,bool 
          rsd,bool 
          rsea,bool 
          rseb,bool 
          rsec,bool 
          rsed,bool 
          rsee,bool 
          rsef,bool 
          rseg,bool 
          rseh,bool 
          rsei,bool 
          rsem,bool 
          rsen,bool 
          rseo,bool 
          rsep,bool 
          rser,bool 
          rses,bool 
          rset,bool 
          rseu,bool 
          rsev,bool 
          rsex,bool 
          rsey,bool 
          rsel,bool 
          rsez,bool 
          rcell,bool 
          rsysmenu,bool 
          sysadmin,String 
          old_id,int 
          savertime,bool 
          localprt,bool 
          rdc,bool 
          ropr,bool 
          rare,bool 
          rse1,String 
          compress,bool 
          rmru,bool 
          rmrud,bool 
          rmrus,bool 
          rul,bool 
          rmrb,bool 
          rmrbt,bool 
          rmrbc,bool 
          rmrbd,bool 
          rmrbcl,bool 
          rsh,bool 
          pradmin,bool 
          unlimited,bool 
          rse2,bool 
          rse3,bool 
          rse4,bool 
          rse5,bool 
          rmr1,bool 
          rmi,bool 
          rmim,bool 
          rmir,bool 
          rmis,bool 
          rmird,bool 
          rmirs,bool 
          ropl,bool 
          ropt,bool 
          rmr2,bool 
          rmr3,bool 
          rmrba,bool 
          roph,bool 
          rmr4,bool 
          rse6,int 
          company_id,bool 
          dealer
        )
        {
        
          m_userid = userid;
        
          m_user = user;
        
          m_login_name = login_name;
        
          m_password = password;
        
          m_department = department;
        
          m_active = active;
        
          m_online = online;
        
          m_ro = ro;
        
          m_roc = roc;
        
          m_rom = rom;
        
          m_ron = ron;
        
          m_ror = ror;
        
          m_row = row;
        
          m_rou = rou;
        
          m_roua = roua;
        
          m_rouk = rouk;
        
          m_roud = roud;
        
          m_rouh = rouh;
        
          m_rouj = rouj;
        
          m_roul = roul;
        
          m_rour = rour;
        
          m_rous = rous;
        
          m_rouc = rouc;
        
          m_roum = roum;
        
          m_rout = rout;
        
          m_roux = roux;
        
          m_rop = rop;
        
          m_ropa = ropa;
        
          m_ropf = ropf;
        
          m_ropd = ropd;
        
          m_ropc = ropc;
        
          m_rops = rops;
        
          m_ropn = ropn;
        
          m_rd = rd;
        
          m_rde = rde;
        
          m_rdq = rdq;
        
          m_rdr = rdr;
        
          m_rds = rds;
        
          m_rdu = rdu;
        
          m_rdua = rdua;
        
          m_rduc = rduc;
        
          m_rdud = rdud;
        
          m_rdup = rdup;
        
          m_rdus = rdus;
        
          m_rdp = rdp;
        
          m_rdpc = rdpc;
        
          m_rdpd = rdpd;
        
          m_rdpi = rdpi;
        
          m_rdpl = rdpl;
        
          m_rdpo = rdpo;
        
          m_rdpp = rdpp;
        
          m_rdps = rdps;
        
          m_rdpb = rdpb;
        
          m_rdpn = rdpn;
        
          m_rdpq = rdpq;
        
          m_rdpu = rdpu;
        
          m_rdpa = rdpa;
        
          m_rdpaa = rdpaa;
        
          m_rdp1 = rdp1;
        
          m_rdj = rdj;
        
          m_ra = ra;
        
          m_rac = rac;
        
          m_rap = rap;
        
          m_rapr = rapr;
        
          m_rapa = rapa;
        
          m_rapt = rapt;
        
          m_rapp = rapp;
        
          m_rapf = rapf;
        
          m_rapc = rapc;
        
          m_rapcb = rapcb;
        
          m_rapcbt = rapcbt;
        
          m_rapcbe = rapcbe;
        
          m_rapcbo = rapcbo;
        
          m_rapcba = rapcba;
        
          m_rapcbp = rapcbp;
        
          m_rapcbg = rapcbg;
        
          m_rapcbj = rapcbj;
        
          m_rapcbr = rapcbr;
        
          m_rapcbrp = rapcbrp;
        
          m_rapcp = rapcp;
        
          m_rapcps = rapcps;
        
          m_rapcpc = rapcpc;
        
          m_rapcpr = rapcpr;
        
          m_rapcprp = rapcprp;
        
          m_rau = rau;
        
          m_raua = raua;
        
          m_rauc = rauc;
        
          m_raue = raue;
        
          m_rauh = rauh;
        
          m_raus = raus;
        
          m_raum = raum;
        
          m_rar = rar;
        
          m_rar1 = rar1;
        
          m_rar2 = rar2;
        
          m_rara = rara;
        
          m_rars = rars;
        
          m_rard = rard;
        
          m_rarh = rarh;
        
          m_rarl = rarl;
        
          m_rarm = rarm;
        
          m_rarn = rarn;
        
          m_raru = raru;
        
          m_rarg = rarg;
        
          m_rarc = rarc;
        
          m_rarr = rarr;
        
          m_rarb = rarb;
        
          m_rart = rart;
        
          m_rari = rari;
        
          m_rarz = rarz;
        
          m_rark = rark;
        
          m_rarw = rarw;
        
          m_raro = raro;
        
          m_rarp = rarp;
        
          m_ru = ru;
        
          m_rui = rui;
        
          m_ruo = ruo;
        
          m_rue = rue;
        
          m_ruq = ruq;
        
          m_ruw = ruw;
        
          m_ruc = ruc;
        
          m_rum = rum;
        
          m_rus = rus;
        
          m_rm = rm;
        
          m_rmr = rmr;
        
          m_rmra = rmra;
        
          m_rmrs = rmrs;
        
          m_rmrd = rmrd;
        
          m_rmrm = rmrm;
        
          m_rmrn = rmrn;
        
          m_rmrr = rmrr;
        
          m_rmrk = rmrk;
        
          m_rmrt = rmrt;
        
          m_rmrj = rmrj;
        
          m_rmrw = rmrw;
        
          m_rmrv = rmrv;
        
          m_rmre = rmre;
        
          m_rmrp = rmrp;
        
          m_rmro = rmro;
        
          m_rmrl = rmrl;
        
          m_rmry = rmry;
        
          m_rmrc = rmrc;
        
          m_rmrf = rmrf;
        
          m_rmg = rmg;
        
          m_rmp = rmp;
        
          m_rs = rs;
        
          m_rss = rss;
        
          m_rssa = rssa;
        
          m_rsse = rsse;
        
          m_rssr = rssr;
        
          m_rssu = rssu;
        
          m_rssz = rssz;
        
          m_rse = rse;
        
          m_rsu = rsu;
        
          m_rsc = rsc;
        
          m_rsi = rsi;
        
          m_rso = rso;
        
          m_rsl = rsl;
        
          m_rsv = rsv;
        
          m_rsd = rsd;
        
          m_rsea = rsea;
        
          m_rseb = rseb;
        
          m_rsec = rsec;
        
          m_rsed = rsed;
        
          m_rsee = rsee;
        
          m_rsef = rsef;
        
          m_rseg = rseg;
        
          m_rseh = rseh;
        
          m_rsei = rsei;
        
          m_rsem = rsem;
        
          m_rsen = rsen;
        
          m_rseo = rseo;
        
          m_rsep = rsep;
        
          m_rser = rser;
        
          m_rses = rses;
        
          m_rset = rset;
        
          m_rseu = rseu;
        
          m_rsev = rsev;
        
          m_rsex = rsex;
        
          m_rsey = rsey;
        
          m_rsel = rsel;
        
          m_rsez = rsez;
        
          m_rcell = rcell;
        
          m_rsysmenu = rsysmenu;
        
          m_sysadmin = sysadmin;
        
          m_old_id = old_id;
        
          m_savertime = savertime;
        
          m_localprt = localprt;
        
          m_rdc = rdc;
        
          m_ropr = ropr;
        
          m_rare = rare;
        
          m_rse1 = rse1;
        
          m_compress = compress;
        
          m_rmru = rmru;
        
          m_rmrud = rmrud;
        
          m_rmrus = rmrus;
        
          m_rul = rul;
        
          m_rmrb = rmrb;
        
          m_rmrbt = rmrbt;
        
          m_rmrbc = rmrbc;
        
          m_rmrbd = rmrbd;
        
          m_rmrbcl = rmrbcl;
        
          m_rsh = rsh;
        
          m_pradmin = pradmin;
        
          m_unlimited = unlimited;
        
          m_rse2 = rse2;
        
          m_rse3 = rse3;
        
          m_rse4 = rse4;
        
          m_rse5 = rse5;
        
          m_rmr1 = rmr1;
        
          m_rmi = rmi;
        
          m_rmim = rmim;
        
          m_rmir = rmir;
        
          m_rmis = rmis;
        
          m_rmird = rmird;
        
          m_rmirs = rmirs;
        
          m_ropl = ropl;
        
          m_ropt = ropt;
        
          m_rmr2 = rmr2;
        
          m_rmr3 = rmr3;
        
          m_rmrba = rmrba;
        
          m_roph = roph;
        
          m_rmr4 = rmr4;
        
          m_rse6 = rse6;
        
          m_company_id = company_id;
        
          m_dealer = dealer;
        
        }


      
      #endregion

      
        [XmlElement]
        public String userid
        {
        get { return m_userid;}
        set { m_userid = value; }
        }
      
        [XmlElement]
        public String user
        {
        get { return m_user;}
        set { m_user = value; }
        }
      
        [XmlElement]
        public String login_name
        {
        get { return m_login_name;}
        set { m_login_name = value; }
        }
      
        [XmlElement]
        public String password
        {
        get { return m_password;}
        set { m_password = value; }
        }
      
        [XmlElement]
        public int department
        {
        get { return m_department;}
        set { m_department = value; }
        }
      
        [XmlElement]
        public bool active
        {
        get { return m_active;}
        set { m_active = value; }
        }
      
        [XmlElement]
        public bool online
        {
        get { return m_online;}
        set { m_online = value; }
        }
      
        [XmlElement]
        public bool ro
        {
        get { return m_ro;}
        set { m_ro = value; }
        }
      
        [XmlElement]
        public bool roc
        {
        get { return m_roc;}
        set { m_roc = value; }
        }
      
        [XmlElement]
        public bool rom
        {
        get { return m_rom;}
        set { m_rom = value; }
        }
      
        [XmlElement]
        public bool ron
        {
        get { return m_ron;}
        set { m_ron = value; }
        }
      
        [XmlElement]
        public bool ror
        {
        get { return m_ror;}
        set { m_ror = value; }
        }
      
        [XmlElement]
        public bool row
        {
        get { return m_row;}
        set { m_row = value; }
        }
      
        [XmlElement]
        public bool rou
        {
        get { return m_rou;}
        set { m_rou = value; }
        }
      
        [XmlElement]
        public bool roua
        {
        get { return m_roua;}
        set { m_roua = value; }
        }
      
        [XmlElement]
        public bool rouk
        {
        get { return m_rouk;}
        set { m_rouk = value; }
        }
      
        [XmlElement]
        public bool roud
        {
        get { return m_roud;}
        set { m_roud = value; }
        }
      
        [XmlElement]
        public bool rouh
        {
        get { return m_rouh;}
        set { m_rouh = value; }
        }
      
        [XmlElement]
        public bool rouj
        {
        get { return m_rouj;}
        set { m_rouj = value; }
        }
      
        [XmlElement]
        public bool roul
        {
        get { return m_roul;}
        set { m_roul = value; }
        }
      
        [XmlElement]
        public bool rour
        {
        get { return m_rour;}
        set { m_rour = value; }
        }
      
        [XmlElement]
        public bool rous
        {
        get { return m_rous;}
        set { m_rous = value; }
        }
      
        [XmlElement]
        public bool rouc
        {
        get { return m_rouc;}
        set { m_rouc = value; }
        }
      
        [XmlElement]
        public bool roum
        {
        get { return m_roum;}
        set { m_roum = value; }
        }
      
        [XmlElement]
        public bool rout
        {
        get { return m_rout;}
        set { m_rout = value; }
        }
      
        [XmlElement]
        public bool roux
        {
        get { return m_roux;}
        set { m_roux = value; }
        }
      
        [XmlElement]
        public bool rop
        {
        get { return m_rop;}
        set { m_rop = value; }
        }
      
        [XmlElement]
        public bool ropa
        {
        get { return m_ropa;}
        set { m_ropa = value; }
        }
      
        [XmlElement]
        public bool ropf
        {
        get { return m_ropf;}
        set { m_ropf = value; }
        }
      
        [XmlElement]
        public bool ropd
        {
        get { return m_ropd;}
        set { m_ropd = value; }
        }
      
        [XmlElement]
        public bool ropc
        {
        get { return m_ropc;}
        set { m_ropc = value; }
        }
      
        [XmlElement]
        public bool rops
        {
        get { return m_rops;}
        set { m_rops = value; }
        }
      
        [XmlElement]
        public bool ropn
        {
        get { return m_ropn;}
        set { m_ropn = value; }
        }
      
        [XmlElement]
        public bool rd
        {
        get { return m_rd;}
        set { m_rd = value; }
        }
      
        [XmlElement]
        public bool rde
        {
        get { return m_rde;}
        set { m_rde = value; }
        }
      
        [XmlElement]
        public bool rdq
        {
        get { return m_rdq;}
        set { m_rdq = value; }
        }
      
        [XmlElement]
        public bool rdr
        {
        get { return m_rdr;}
        set { m_rdr = value; }
        }
      
        [XmlElement]
        public bool rds
        {
        get { return m_rds;}
        set { m_rds = value; }
        }
      
        [XmlElement]
        public bool rdu
        {
        get { return m_rdu;}
        set { m_rdu = value; }
        }
      
        [XmlElement]
        public bool rdua
        {
        get { return m_rdua;}
        set { m_rdua = value; }
        }
      
        [XmlElement]
        public bool rduc
        {
        get { return m_rduc;}
        set { m_rduc = value; }
        }
      
        [XmlElement]
        public bool rdud
        {
        get { return m_rdud;}
        set { m_rdud = value; }
        }
      
        [XmlElement]
        public bool rdup
        {
        get { return m_rdup;}
        set { m_rdup = value; }
        }
      
        [XmlElement]
        public bool rdus
        {
        get { return m_rdus;}
        set { m_rdus = value; }
        }
      
        [XmlElement]
        public bool rdp
        {
        get { return m_rdp;}
        set { m_rdp = value; }
        }
      
        [XmlElement]
        public bool rdpc
        {
        get { return m_rdpc;}
        set { m_rdpc = value; }
        }
      
        [XmlElement]
        public bool rdpd
        {
        get { return m_rdpd;}
        set { m_rdpd = value; }
        }
      
        [XmlElement]
        public bool rdpi
        {
        get { return m_rdpi;}
        set { m_rdpi = value; }
        }
      
        [XmlElement]
        public bool rdpl
        {
        get { return m_rdpl;}
        set { m_rdpl = value; }
        }
      
        [XmlElement]
        public bool rdpo
        {
        get { return m_rdpo;}
        set { m_rdpo = value; }
        }
      
        [XmlElement]
        public bool rdpp
        {
        get { return m_rdpp;}
        set { m_rdpp = value; }
        }
      
        [XmlElement]
        public bool rdps
        {
        get { return m_rdps;}
        set { m_rdps = value; }
        }
      
        [XmlElement]
        public bool rdpb
        {
        get { return m_rdpb;}
        set { m_rdpb = value; }
        }
      
        [XmlElement]
        public bool rdpn
        {
        get { return m_rdpn;}
        set { m_rdpn = value; }
        }
      
        [XmlElement]
        public bool rdpq
        {
        get { return m_rdpq;}
        set { m_rdpq = value; }
        }
      
        [XmlElement]
        public bool rdpu
        {
        get { return m_rdpu;}
        set { m_rdpu = value; }
        }
      
        [XmlElement]
        public bool rdpa
        {
        get { return m_rdpa;}
        set { m_rdpa = value; }
        }
      
        [XmlElement]
        public bool rdpaa
        {
        get { return m_rdpaa;}
        set { m_rdpaa = value; }
        }
      
        [XmlElement]
        public bool rdp1
        {
        get { return m_rdp1;}
        set { m_rdp1 = value; }
        }
      
        [XmlElement]
        public bool rdj
        {
        get { return m_rdj;}
        set { m_rdj = value; }
        }
      
        [XmlElement]
        public bool ra
        {
        get { return m_ra;}
        set { m_ra = value; }
        }
      
        [XmlElement]
        public bool rac
        {
        get { return m_rac;}
        set { m_rac = value; }
        }
      
        [XmlElement]
        public bool rap
        {
        get { return m_rap;}
        set { m_rap = value; }
        }
      
        [XmlElement]
        public bool rapr
        {
        get { return m_rapr;}
        set { m_rapr = value; }
        }
      
        [XmlElement]
        public bool rapa
        {
        get { return m_rapa;}
        set { m_rapa = value; }
        }
      
        [XmlElement]
        public bool rapt
        {
        get { return m_rapt;}
        set { m_rapt = value; }
        }
      
        [XmlElement]
        public bool rapp
        {
        get { return m_rapp;}
        set { m_rapp = value; }
        }
      
        [XmlElement]
        public bool rapf
        {
        get { return m_rapf;}
        set { m_rapf = value; }
        }
      
        [XmlElement]
        public bool rapc
        {
        get { return m_rapc;}
        set { m_rapc = value; }
        }
      
        [XmlElement]
        public bool rapcb
        {
        get { return m_rapcb;}
        set { m_rapcb = value; }
        }
      
        [XmlElement]
        public bool rapcbt
        {
        get { return m_rapcbt;}
        set { m_rapcbt = value; }
        }
      
        [XmlElement]
        public bool rapcbe
        {
        get { return m_rapcbe;}
        set { m_rapcbe = value; }
        }
      
        [XmlElement]
        public bool rapcbo
        {
        get { return m_rapcbo;}
        set { m_rapcbo = value; }
        }
      
        [XmlElement]
        public bool rapcba
        {
        get { return m_rapcba;}
        set { m_rapcba = value; }
        }
      
        [XmlElement]
        public bool rapcbp
        {
        get { return m_rapcbp;}
        set { m_rapcbp = value; }
        }
      
        [XmlElement]
        public bool rapcbg
        {
        get { return m_rapcbg;}
        set { m_rapcbg = value; }
        }
      
        [XmlElement]
        public bool rapcbj
        {
        get { return m_rapcbj;}
        set { m_rapcbj = value; }
        }
      
        [XmlElement]
        public bool rapcbr
        {
        get { return m_rapcbr;}
        set { m_rapcbr = value; }
        }
      
        [XmlElement]
        public bool rapcbrp
        {
        get { return m_rapcbrp;}
        set { m_rapcbrp = value; }
        }
      
        [XmlElement]
        public bool rapcp
        {
        get { return m_rapcp;}
        set { m_rapcp = value; }
        }
      
        [XmlElement]
        public bool rapcps
        {
        get { return m_rapcps;}
        set { m_rapcps = value; }
        }
      
        [XmlElement]
        public bool rapcpc
        {
        get { return m_rapcpc;}
        set { m_rapcpc = value; }
        }
      
        [XmlElement]
        public bool rapcpr
        {
        get { return m_rapcpr;}
        set { m_rapcpr = value; }
        }
      
        [XmlElement]
        public bool rapcprp
        {
        get { return m_rapcprp;}
        set { m_rapcprp = value; }
        }
      
        [XmlElement]
        public bool rau
        {
        get { return m_rau;}
        set { m_rau = value; }
        }
      
        [XmlElement]
        public bool raua
        {
        get { return m_raua;}
        set { m_raua = value; }
        }
      
        [XmlElement]
        public bool rauc
        {
        get { return m_rauc;}
        set { m_rauc = value; }
        }
      
        [XmlElement]
        public bool raue
        {
        get { return m_raue;}
        set { m_raue = value; }
        }
      
        [XmlElement]
        public bool rauh
        {
        get { return m_rauh;}
        set { m_rauh = value; }
        }
      
        [XmlElement]
        public bool raus
        {
        get { return m_raus;}
        set { m_raus = value; }
        }
      
        [XmlElement]
        public bool raum
        {
        get { return m_raum;}
        set { m_raum = value; }
        }
      
        [XmlElement]
        public bool rar
        {
        get { return m_rar;}
        set { m_rar = value; }
        }
      
        [XmlElement]
        public bool rar1
        {
        get { return m_rar1;}
        set { m_rar1 = value; }
        }
      
        [XmlElement]
        public bool rar2
        {
        get { return m_rar2;}
        set { m_rar2 = value; }
        }
      
        [XmlElement]
        public bool rara
        {
        get { return m_rara;}
        set { m_rara = value; }
        }
      
        [XmlElement]
        public bool rars
        {
        get { return m_rars;}
        set { m_rars = value; }
        }
      
        [XmlElement]
        public bool rard
        {
        get { return m_rard;}
        set { m_rard = value; }
        }
      
        [XmlElement]
        public bool rarh
        {
        get { return m_rarh;}
        set { m_rarh = value; }
        }
      
        [XmlElement]
        public bool rarl
        {
        get { return m_rarl;}
        set { m_rarl = value; }
        }
      
        [XmlElement]
        public bool rarm
        {
        get { return m_rarm;}
        set { m_rarm = value; }
        }
      
        [XmlElement]
        public bool rarn
        {
        get { return m_rarn;}
        set { m_rarn = value; }
        }
      
        [XmlElement]
        public bool raru
        {
        get { return m_raru;}
        set { m_raru = value; }
        }
      
        [XmlElement]
        public bool rarg
        {
        get { return m_rarg;}
        set { m_rarg = value; }
        }
      
        [XmlElement]
        public bool rarc
        {
        get { return m_rarc;}
        set { m_rarc = value; }
        }
      
        [XmlElement]
        public bool rarr
        {
        get { return m_rarr;}
        set { m_rarr = value; }
        }
      
        [XmlElement]
        public bool rarb
        {
        get { return m_rarb;}
        set { m_rarb = value; }
        }
      
        [XmlElement]
        public bool rart
        {
        get { return m_rart;}
        set { m_rart = value; }
        }
      
        [XmlElement]
        public bool rari
        {
        get { return m_rari;}
        set { m_rari = value; }
        }
      
        [XmlElement]
        public bool rarz
        {
        get { return m_rarz;}
        set { m_rarz = value; }
        }
      
        [XmlElement]
        public bool rark
        {
        get { return m_rark;}
        set { m_rark = value; }
        }
      
        [XmlElement]
        public bool rarw
        {
        get { return m_rarw;}
        set { m_rarw = value; }
        }
      
        [XmlElement]
        public bool raro
        {
        get { return m_raro;}
        set { m_raro = value; }
        }
      
        [XmlElement]
        public bool rarp
        {
        get { return m_rarp;}
        set { m_rarp = value; }
        }
      
        [XmlElement]
        public bool ru
        {
        get { return m_ru;}
        set { m_ru = value; }
        }
      
        [XmlElement]
        public bool rui
        {
        get { return m_rui;}
        set { m_rui = value; }
        }
      
        [XmlElement]
        public bool ruo
        {
        get { return m_ruo;}
        set { m_ruo = value; }
        }
      
        [XmlElement]
        public bool rue
        {
        get { return m_rue;}
        set { m_rue = value; }
        }
      
        [XmlElement]
        public bool ruq
        {
        get { return m_ruq;}
        set { m_ruq = value; }
        }
      
        [XmlElement]
        public bool ruw
        {
        get { return m_ruw;}
        set { m_ruw = value; }
        }
      
        [XmlElement]
        public bool ruc
        {
        get { return m_ruc;}
        set { m_ruc = value; }
        }
      
        [XmlElement]
        public bool rum
        {
        get { return m_rum;}
        set { m_rum = value; }
        }
      
        [XmlElement]
        public bool rus
        {
        get { return m_rus;}
        set { m_rus = value; }
        }
      
        [XmlElement]
        public bool rm
        {
        get { return m_rm;}
        set { m_rm = value; }
        }
      
        [XmlElement]
        public bool rmr
        {
        get { return m_rmr;}
        set { m_rmr = value; }
        }
      
        [XmlElement]
        public bool rmra
        {
        get { return m_rmra;}
        set { m_rmra = value; }
        }
      
        [XmlElement]
        public bool rmrs
        {
        get { return m_rmrs;}
        set { m_rmrs = value; }
        }
      
        [XmlElement]
        public bool rmrd
        {
        get { return m_rmrd;}
        set { m_rmrd = value; }
        }
      
        [XmlElement]
        public bool rmrm
        {
        get { return m_rmrm;}
        set { m_rmrm = value; }
        }
      
        [XmlElement]
        public bool rmrn
        {
        get { return m_rmrn;}
        set { m_rmrn = value; }
        }
      
        [XmlElement]
        public bool rmrr
        {
        get { return m_rmrr;}
        set { m_rmrr = value; }
        }
      
        [XmlElement]
        public bool rmrk
        {
        get { return m_rmrk;}
        set { m_rmrk = value; }
        }
      
        [XmlElement]
        public bool rmrt
        {
        get { return m_rmrt;}
        set { m_rmrt = value; }
        }
      
        [XmlElement]
        public bool rmrj
        {
        get { return m_rmrj;}
        set { m_rmrj = value; }
        }
      
        [XmlElement]
        public bool rmrw
        {
        get { return m_rmrw;}
        set { m_rmrw = value; }
        }
      
        [XmlElement]
        public bool rmrv
        {
        get { return m_rmrv;}
        set { m_rmrv = value; }
        }
      
        [XmlElement]
        public bool rmre
        {
        get { return m_rmre;}
        set { m_rmre = value; }
        }
      
        [XmlElement]
        public bool rmrp
        {
        get { return m_rmrp;}
        set { m_rmrp = value; }
        }
      
        [XmlElement]
        public bool rmro
        {
        get { return m_rmro;}
        set { m_rmro = value; }
        }
      
        [XmlElement]
        public bool rmrl
        {
        get { return m_rmrl;}
        set { m_rmrl = value; }
        }
      
        [XmlElement]
        public bool rmry
        {
        get { return m_rmry;}
        set { m_rmry = value; }
        }
      
        [XmlElement]
        public bool rmrc
        {
        get { return m_rmrc;}
        set { m_rmrc = value; }
        }
      
        [XmlElement]
        public bool rmrf
        {
        get { return m_rmrf;}
        set { m_rmrf = value; }
        }
      
        [XmlElement]
        public bool rmg
        {
        get { return m_rmg;}
        set { m_rmg = value; }
        }
      
        [XmlElement]
        public bool rmp
        {
        get { return m_rmp;}
        set { m_rmp = value; }
        }
      
        [XmlElement]
        public bool rs
        {
        get { return m_rs;}
        set { m_rs = value; }
        }
      
        [XmlElement]
        public bool rss
        {
        get { return m_rss;}
        set { m_rss = value; }
        }
      
        [XmlElement]
        public bool rssa
        {
        get { return m_rssa;}
        set { m_rssa = value; }
        }
      
        [XmlElement]
        public bool rsse
        {
        get { return m_rsse;}
        set { m_rsse = value; }
        }
      
        [XmlElement]
        public bool rssr
        {
        get { return m_rssr;}
        set { m_rssr = value; }
        }
      
        [XmlElement]
        public bool rssu
        {
        get { return m_rssu;}
        set { m_rssu = value; }
        }
      
        [XmlElement]
        public bool rssz
        {
        get { return m_rssz;}
        set { m_rssz = value; }
        }
      
        [XmlElement]
        public bool rse
        {
        get { return m_rse;}
        set { m_rse = value; }
        }
      
        [XmlElement]
        public bool rsu
        {
        get { return m_rsu;}
        set { m_rsu = value; }
        }
      
        [XmlElement]
        public bool rsc
        {
        get { return m_rsc;}
        set { m_rsc = value; }
        }
      
        [XmlElement]
        public bool rsi
        {
        get { return m_rsi;}
        set { m_rsi = value; }
        }
      
        [XmlElement]
        public bool rso
        {
        get { return m_rso;}
        set { m_rso = value; }
        }
      
        [XmlElement]
        public bool rsl
        {
        get { return m_rsl;}
        set { m_rsl = value; }
        }
      
        [XmlElement]
        public bool rsv
        {
        get { return m_rsv;}
        set { m_rsv = value; }
        }
      
        [XmlElement]
        public bool rsd
        {
        get { return m_rsd;}
        set { m_rsd = value; }
        }
      
        [XmlElement]
        public bool rsea
        {
        get { return m_rsea;}
        set { m_rsea = value; }
        }
      
        [XmlElement]
        public bool rseb
        {
        get { return m_rseb;}
        set { m_rseb = value; }
        }
      
        [XmlElement]
        public bool rsec
        {
        get { return m_rsec;}
        set { m_rsec = value; }
        }
      
        [XmlElement]
        public bool rsed
        {
        get { return m_rsed;}
        set { m_rsed = value; }
        }
      
        [XmlElement]
        public bool rsee
        {
        get { return m_rsee;}
        set { m_rsee = value; }
        }
      
        [XmlElement]
        public bool rsef
        {
        get { return m_rsef;}
        set { m_rsef = value; }
        }
      
        [XmlElement]
        public bool rseg
        {
        get { return m_rseg;}
        set { m_rseg = value; }
        }
      
        [XmlElement]
        public bool rseh
        {
        get { return m_rseh;}
        set { m_rseh = value; }
        }
      
        [XmlElement]
        public bool rsei
        {
        get { return m_rsei;}
        set { m_rsei = value; }
        }
      
        [XmlElement]
        public bool rsem
        {
        get { return m_rsem;}
        set { m_rsem = value; }
        }
      
        [XmlElement]
        public bool rsen
        {
        get { return m_rsen;}
        set { m_rsen = value; }
        }
      
        [XmlElement]
        public bool rseo
        {
        get { return m_rseo;}
        set { m_rseo = value; }
        }
      
        [XmlElement]
        public bool rsep
        {
        get { return m_rsep;}
        set { m_rsep = value; }
        }
      
        [XmlElement]
        public bool rser
        {
        get { return m_rser;}
        set { m_rser = value; }
        }
      
        [XmlElement]
        public bool rses
        {
        get { return m_rses;}
        set { m_rses = value; }
        }
      
        [XmlElement]
        public bool rset
        {
        get { return m_rset;}
        set { m_rset = value; }
        }
      
        [XmlElement]
        public bool rseu
        {
        get { return m_rseu;}
        set { m_rseu = value; }
        }
      
        [XmlElement]
        public bool rsev
        {
        get { return m_rsev;}
        set { m_rsev = value; }
        }
      
        [XmlElement]
        public bool rsex
        {
        get { return m_rsex;}
        set { m_rsex = value; }
        }
      
        [XmlElement]
        public bool rsey
        {
        get { return m_rsey;}
        set { m_rsey = value; }
        }
      
        [XmlElement]
        public bool rsel
        {
        get { return m_rsel;}
        set { m_rsel = value; }
        }
      
        [XmlElement]
        public bool rsez
        {
        get { return m_rsez;}
        set { m_rsez = value; }
        }
      
        [XmlElement]
        public bool rcell
        {
        get { return m_rcell;}
        set { m_rcell = value; }
        }
      
        [XmlElement]
        public bool rsysmenu
        {
        get { return m_rsysmenu;}
        set { m_rsysmenu = value; }
        }
      
        [XmlElement]
        public bool sysadmin
        {
        get { return m_sysadmin;}
        set { m_sysadmin = value; }
        }
      
        [XmlElement]
        public String old_id
        {
        get { return m_old_id;}
        set { m_old_id = value; }
        }
      
        [XmlElement]
        public int savertime
        {
        get { return m_savertime;}
        set { m_savertime = value; }
        }
      
        [XmlElement]
        public bool localprt
        {
        get { return m_localprt;}
        set { m_localprt = value; }
        }
      
        [XmlElement]
        public bool rdc
        {
        get { return m_rdc;}
        set { m_rdc = value; }
        }
      
        [XmlElement]
        public bool ropr
        {
        get { return m_ropr;}
        set { m_ropr = value; }
        }
      
        [XmlElement]
        public bool rare
        {
        get { return m_rare;}
        set { m_rare = value; }
        }
      
        [XmlElement]
        public bool rse1
        {
        get { return m_rse1;}
        set { m_rse1 = value; }
        }
      
        [XmlElement]
        public String compress
        {
        get { return m_compress;}
        set { m_compress = value; }
        }
      
        [XmlElement]
        public bool rmru
        {
        get { return m_rmru;}
        set { m_rmru = value; }
        }
      
        [XmlElement]
        public bool rmrud
        {
        get { return m_rmrud;}
        set { m_rmrud = value; }
        }
      
        [XmlElement]
        public bool rmrus
        {
        get { return m_rmrus;}
        set { m_rmrus = value; }
        }
      
        [XmlElement]
        public bool rul
        {
        get { return m_rul;}
        set { m_rul = value; }
        }
      
        [XmlElement]
        public bool rmrb
        {
        get { return m_rmrb;}
        set { m_rmrb = value; }
        }
      
        [XmlElement]
        public bool rmrbt
        {
        get { return m_rmrbt;}
        set { m_rmrbt = value; }
        }
      
        [XmlElement]
        public bool rmrbc
        {
        get { return m_rmrbc;}
        set { m_rmrbc = value; }
        }
      
        [XmlElement]
        public bool rmrbd
        {
        get { return m_rmrbd;}
        set { m_rmrbd = value; }
        }
      
        [XmlElement]
        public bool rmrbcl
        {
        get { return m_rmrbcl;}
        set { m_rmrbcl = value; }
        }
      
        [XmlElement]
        public bool rsh
        {
        get { return m_rsh;}
        set { m_rsh = value; }
        }
      
        [XmlElement]
        public bool pradmin
        {
        get { return m_pradmin;}
        set { m_pradmin = value; }
        }
      
        [XmlElement]
        public bool unlimited
        {
        get { return m_unlimited;}
        set { m_unlimited = value; }
        }
      
        [XmlElement]
        public bool rse2
        {
        get { return m_rse2;}
        set { m_rse2 = value; }
        }
      
        [XmlElement]
        public bool rse3
        {
        get { return m_rse3;}
        set { m_rse3 = value; }
        }
      
        [XmlElement]
        public bool rse4
        {
        get { return m_rse4;}
        set { m_rse4 = value; }
        }
      
        [XmlElement]
        public bool rse5
        {
        get { return m_rse5;}
        set { m_rse5 = value; }
        }
      
        [XmlElement]
        public bool rmr1
        {
        get { return m_rmr1;}
        set { m_rmr1 = value; }
        }
      
        [XmlElement]
        public bool rmi
        {
        get { return m_rmi;}
        set { m_rmi = value; }
        }
      
        [XmlElement]
        public bool rmim
        {
        get { return m_rmim;}
        set { m_rmim = value; }
        }
      
        [XmlElement]
        public bool rmir
        {
        get { return m_rmir;}
        set { m_rmir = value; }
        }
      
        [XmlElement]
        public bool rmis
        {
        get { return m_rmis;}
        set { m_rmis = value; }
        }
      
        [XmlElement]
        public bool rmird
        {
        get { return m_rmird;}
        set { m_rmird = value; }
        }
      
        [XmlElement]
        public bool rmirs
        {
        get { return m_rmirs;}
        set { m_rmirs = value; }
        }
      
        [XmlElement]
        public bool ropl
        {
        get { return m_ropl;}
        set { m_ropl = value; }
        }
      
        [XmlElement]
        public bool ropt
        {
        get { return m_ropt;}
        set { m_ropt = value; }
        }
      
        [XmlElement]
        public bool rmr2
        {
        get { return m_rmr2;}
        set { m_rmr2 = value; }
        }
      
        [XmlElement]
        public bool rmr3
        {
        get { return m_rmr3;}
        set { m_rmr3 = value; }
        }
      
        [XmlElement]
        public bool rmrba
        {
        get { return m_rmrba;}
        set { m_rmrba = value; }
        }
      
        [XmlElement]
        public bool roph
        {
        get { return m_roph;}
        set { m_roph = value; }
        }
      
        [XmlElement]
        public bool rmr4
        {
        get { return m_rmr4;}
        set { m_rmr4 = value; }
        }
      
        [XmlElement]
        public bool rse6
        {
        get { return m_rse6;}
        set { m_rse6 = value; }
        }
      
        [XmlElement]
        public int company_id
        {
        get { return m_company_id;}
        set { m_company_id = value; }
        }
      
        [XmlElement]
        public bool dealer
        {
        get { return m_dealer;}
        set { m_dealer = value; }
        }
      
      }
      #endregion
      }

    