using System;
using System.Collections.Generic;
using SmartManager.Domain.Validators;

namespace SmartManager.Domain.Entities {
    public class User : Base {
      
        public string ?Name { get; private set; } 

        public string ?Email { get; private set; } 

        public string ?Password { get; private set; } 

        protected User() { }

        public User(string ?name, string ?email, string ?password)
        {
            Name = name;
            Email = email;
            Password = password;
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

                throw new Exception("Campos Inv�lidos" + _errors[0]);
            }

            return true;
        }
    }
}