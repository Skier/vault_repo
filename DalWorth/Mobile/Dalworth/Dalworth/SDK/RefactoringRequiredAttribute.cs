using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Dalworth.SDK
{
    public class RefactoringRequiredAttribute:Attribute
    {
        String m_description;

        public RefactoringRequiredAttribute()
        {
        }

        public RefactoringRequiredAttribute(String description)
        {
            m_description = description;
        }
    }
}
