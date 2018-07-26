using AutoMapper;
using Domains.Identity.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Identity.Models
{
    public class UserCreateModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }

    public class UserCreateModelMapper : Profile
    {
        public UserCreateModelMapper() 
        {
            CreateMap<UserCreateModel, User>()
                .ForSourceMember(s => s.RePassword, opt => opt.Ignore())
                .ForSourceMember(s => s.Role, opt => opt.Ignore());
        }
    }

    public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(p => p.UserName).NotEmpty();
            RuleFor(p => p.Email).NotEmpty().EmailAddress();
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
            RuleFor(p => p.RePassword).NotEmpty();
            RuleFor(p => p.Password).Equal(p => p.RePassword).WithMessage("Password and Re-password are not match");
            RuleFor(p => p.Password).MinimumLength(8);
            RuleFor(p => p.Password).RequireDigit();
            RuleFor(p => p.Password).RequireUppercase();
            // Rule for ASCII Punctuation & Symbols(special characters)
            RuleFor(p => p.Password).RequirePunctuation();
            RuleFor(p => p.Role).NotEmpty();
        }
    }
}
