using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.Results;

namespace CoreFaces.Identity.Helper
{
    public static class FluentValidationHelper
    {
        public static List<ValidationFailure> GenerateErrorList(List<String> errList)
        {
            List<ValidationFailure> list = new List<ValidationFailure>();
            foreach (string item in errList)
            {
                list.Add(new ValidationFailure("", item));
            }
            return list;
        }

        public static List<ValidationFailure> GenerateErrorList(string err)
        {
            List<ValidationFailure> list = new List<ValidationFailure>();
            list.Add(new ValidationFailure("", err));
            return list;
        }
    }
}
