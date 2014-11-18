using System;

namespace Dalworth.Common.SDK
{
    public class RefactoringRequiredAttribute:Attribute
    {
        String Description;

        public RefactoringRequiredAttribute()
        {
        }

        public RefactoringRequiredAttribute(String description)
        {
            Description = description;
        }
    }
}
