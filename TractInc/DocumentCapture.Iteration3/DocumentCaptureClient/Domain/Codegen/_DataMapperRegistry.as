
     package Domain.Codegen
     {
      
        import Domain.AddresstypeDataMapper;
      
        import Domain.CountysDataMapper;
      
        import Domain.DocumentDataMapper;
      
        import Domain.DocumenttypeDataMapper;
      
        import Domain.ParticipantDataMapper;
      
        import Domain.ParticipantaddressDataMapper;
      
        import Domain.ParticipantentitypartyDataMapper;
      
        import Domain.ParticipantreservationDataMapper;
      
        import Domain.ParticipantroleDataMapper;
      
        import Domain.ParticipanttypeDataMapper;
      
        import Domain.StatesDataMapper;
      
        import Domain.TractDataMapper;
      
        import Domain.TractexceptionDataMapper;
      
       public class _DataMapperRegistry
       {
        

          private var m_addresstypeDataMapper:AddresstypeDataMapper;

          public function get Addresstype():AddresstypeDataMapper
          {
            if(m_addresstypeDataMapper == null )
              m_addresstypeDataMapper = new AddresstypeDataMapper();
              
            return m_addresstypeDataMapper;
          }
        

          private var m_countysDataMapper:CountysDataMapper;

          public function get Countys():CountysDataMapper
          {
            if(m_countysDataMapper == null )
              m_countysDataMapper = new CountysDataMapper();
              
            return m_countysDataMapper;
          }
        

          private var m_documentDataMapper:DocumentDataMapper;

          public function get Document():DocumentDataMapper
          {
            if(m_documentDataMapper == null )
              m_documentDataMapper = new DocumentDataMapper();
              
            return m_documentDataMapper;
          }
        

          private var m_documenttypeDataMapper:DocumenttypeDataMapper;

          public function get Documenttype():DocumenttypeDataMapper
          {
            if(m_documenttypeDataMapper == null )
              m_documenttypeDataMapper = new DocumenttypeDataMapper();
              
            return m_documenttypeDataMapper;
          }
        

          private var m_participantDataMapper:ParticipantDataMapper;

          public function get Participant():ParticipantDataMapper
          {
            if(m_participantDataMapper == null )
              m_participantDataMapper = new ParticipantDataMapper();
              
            return m_participantDataMapper;
          }
        

          private var m_participantaddressDataMapper:ParticipantaddressDataMapper;

          public function get Participantaddress():ParticipantaddressDataMapper
          {
            if(m_participantaddressDataMapper == null )
              m_participantaddressDataMapper = new ParticipantaddressDataMapper();
              
            return m_participantaddressDataMapper;
          }
        

          private var m_participantentitypartyDataMapper:ParticipantentitypartyDataMapper;

          public function get Participantentityparty():ParticipantentitypartyDataMapper
          {
            if(m_participantentitypartyDataMapper == null )
              m_participantentitypartyDataMapper = new ParticipantentitypartyDataMapper();
              
            return m_participantentitypartyDataMapper;
          }
        

          private var m_participantreservationDataMapper:ParticipantreservationDataMapper;

          public function get Participantreservation():ParticipantreservationDataMapper
          {
            if(m_participantreservationDataMapper == null )
              m_participantreservationDataMapper = new ParticipantreservationDataMapper();
              
            return m_participantreservationDataMapper;
          }
        

          private var m_participantroleDataMapper:ParticipantroleDataMapper;

          public function get Participantrole():ParticipantroleDataMapper
          {
            if(m_participantroleDataMapper == null )
              m_participantroleDataMapper = new ParticipantroleDataMapper();
              
            return m_participantroleDataMapper;
          }
        

          private var m_participanttypeDataMapper:ParticipanttypeDataMapper;

          public function get Participanttype():ParticipanttypeDataMapper
          {
            if(m_participanttypeDataMapper == null )
              m_participanttypeDataMapper = new ParticipanttypeDataMapper();
              
            return m_participanttypeDataMapper;
          }
        

          private var m_statesDataMapper:StatesDataMapper;

          public function get States():StatesDataMapper
          {
            if(m_statesDataMapper == null )
              m_statesDataMapper = new StatesDataMapper();
              
            return m_statesDataMapper;
          }
        

          private var m_tractDataMapper:TractDataMapper;

          public function get Tract():TractDataMapper
          {
            if(m_tractDataMapper == null )
              m_tractDataMapper = new TractDataMapper();
              
            return m_tractDataMapper;
          }
        

          private var m_tractexceptionDataMapper:TractexceptionDataMapper;

          public function get Tractexception():TractexceptionDataMapper
          {
            if(m_tractexceptionDataMapper == null )
              m_tractexceptionDataMapper = new TractexceptionDataMapper();
              
            return m_tractexceptionDataMapper;
          }
            
        }
      }
    