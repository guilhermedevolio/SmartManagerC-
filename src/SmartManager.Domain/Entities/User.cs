using System;
using System.Collections.Generic;
using SmartManager.Core.Exceptions;
using SmartManager.Domain.Validators;

namespace SmartManager.Domain.Entities {
    public class User : Base {
      
        public string ?Name { get; private set; } 

        public string ?Email { get; private set; } 

        public string ?Password { get; private set; } 
        public string ?Role { get; private set; } 
        public int AccessAttempts { get; private set; } 

        public DateTime ?UnlockDate { get; private set; }

        public Boolean IsBlocked => UnlockDate > DateTime.Now;

        protected User()
        {
         
        }

        public User(string? name, string? email, string? password, string? role = null)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            _errors = new List<String>();
        }

        public void changeName(string name)
        {
            this.Name = name;
            Validate();
        }

        public void changeEmail(string email)
        {
            this.Email = email;
            Validate();
        }

        public void changeRole(string role)
        {
            this.Role = role;
            Validate();
        }

        public void changeAccessAttempts(int attemps)
        {
            this.AccessAttempts = attemps;
            Validate();
        }

        public void changeUnlockDate(DateTime time)
        {
            this.UnlockDate = time;
            Validate();
        }


        public override bool Validate()
        {
            var validator = new UserValidator();
            var validation = validator.Validate(this);

            if(!validation.IsValid)
            {
                foreach(var error in validation.Errors)
                {
                    _errors.Add(error.ErrorMessage);
                }

                throw new DomainException("Campos Inv�lidos" + _errors[0]);
            }

            return true;
        }
    }
}