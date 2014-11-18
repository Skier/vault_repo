using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;
using Dalworth.LeadCentral.Web.Models.Validators;

namespace Dalworth.LeadCentral.Web.Models.User
{
    public class UserEdit
    {
        public UserEdit ()
        {
            this.User = new Domain.User();
        }

        public BusinessPartner CurrentPartner { get; set; }

        public Domain.User User { get; set; }
        public int UserId
        {
            get { return User.Id; }
            set { User.Id = value; }
        }

        #region partners

        private List<BusinessPartner> m_partners;

        public bool DenyChangePartner { get; set; }

        public int PartnerId
        {
            get { return !User.BusinessPartnerId.HasValue ? 0 : User.BusinessPartnerId.Value; }
            set
            {
                if (value == 0)
                {
                    User.BusinessPartnerId = null;
                }
                else
                {
                    User.BusinessPartnerId = value;
                    User.QbRoleName = UserRoleEnum.BusinessPartner.ToString();
                }
            }
        }

        public List<BusinessPartner> PartnerList
        {
            get
            {
                var result = new List<BusinessPartner> { new BusinessPartner { Id = 0, PartnerName = string.Empty } };
                if (m_partners != null && m_partners.Count > 0)
                {
                    foreach (var partner in m_partners)
                    {
                        result.Add(partner);
                    }
                }

                return result;
            }
        }

        #endregion

        #region roles

        public List<string> UserRoleList;
        public void InitRoles(string currentUserRole)
        {
            UserRoleList = new List<string>();
            if (currentUserRole == UserRoleEnum.Administrator.ToString())
                UserRoleList.Add(UserRoleEnum.Administrator.ToString());
            if (currentUserRole != UserRoleEnum.BusinessPartner.ToString())
                UserRoleList.Add(UserRoleEnum.Staff.ToString());

            UserRoleList.Add(UserRoleEnum.Accountant.ToString());
            UserRoleList.Add(UserRoleEnum.BusinessPartner.ToString());
        }

        public string UserRole
        {
            get { return User.QbRoleName; }
            set { User.QbRoleName = value; }
        }

        #endregion

        public DateTime UserDateCreated
        {
            get { return User.DateCreated; }
            set { User.DateCreated = value; }
        }

        public bool UserIsActive
        {
            get { return User.IsActive; }
            set { User.IsActive = value; }
        }

        [StringLength(50, ErrorMessage = @"Must be under 50 characters")]
        public string UserScreenName
        {
            get { return User.ScreenName; }
            set { User.ScreenName = value; }
        }

        [Required(ErrorMessage = @"Please enter User's First Name")]
        [StringLength(50, ErrorMessage = @"Must be under 50 characters")]
        public string UserFirstName
        {
            get { return User.FirstName; }
            set { User.FirstName = value; }
        }

        [Required(ErrorMessage = @"Please enter User's Last Name")]
        [StringLength(50, ErrorMessage = @"Must be under 50 characters")]
        public string UserLastName
        {
            get { return User.LastName; }
            set { User.LastName = value; }
        }

        [Required(ErrorMessage = @"Please enter Email")]
        [Email(ErrorMessage = @"Email must be valid")]
        public string UserEmail
        {
            get { return User.Email; }
            set { User.Email = value; }
        }

        [Required(ErrorMessage = @"Please enter Phone number")]
        [Phone(ErrorMessage = @"Phone Number must have valid format. For Example: '(555) 456 1212'")]
        public string UserPhone
        {
            get { return User.Phone; }
            set { User.Phone = value; }
        }

        public void Load(IDbConnection connection, int? currentUserId = null)
        {
            m_partners = BusinessPartnerService.FindActive(connection);
            if (currentUserId.HasValue)
            {
                User = UserService.FindByPrimaryKey(currentUserId.Value, connection);
                DenyChangePartner = User.RelatedBusinessPartner != null;
            }
        }

        public void CreateUser (IDbConnection connection)
        {
            UserService.Create(User, connection);
        }

        public void DeactivateUser (IDbConnection connection)
        {
            UserService.Deactivate(User, connection);
        }

        public void ActivateUser (IDbConnection connection)
        {
            UserService.Activate(User, connection );
        }

        public void Update(IDbConnection connection)
        {
            Domain.User user;
            if (User.Id > 0)
            {
                user = UserService.FindByPrimaryKey(User.Id, connection);

                user.FirstName = UserFirstName;
                user.LastName = UserLastName;
                user.ScreenName = UserScreenName;
                user.Email = UserEmail;
                user.Phone = UserPhone;
            }
            else
                user = User;

            if(user.RelatedBusinessPartner == null)
            {
                user.QbRoleName = UserRole;
                user.BusinessPartnerId = User.BusinessPartnerId;
            }

            UserService.Update(user, connection);
        }
    }
}