﻿using Family.Application.Models.FamilyMember;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Family.Application.Validation
{
    public class FamilyMemberCreateModelValidator : AbstractValidator<FamilyMemberCreateModel>
    {
        public FamilyMemberCreateModelValidator() 
        {
            
        }
    }
}
