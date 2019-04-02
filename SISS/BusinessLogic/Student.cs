using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    class Student
    {
        private string Enrollment { get; set; }
        private string Password { get; set; }
        private string Name { get; set; }
        private string Email { get; set; }
        private string Career { get; set; }
        private int Section { get; set; }
        private int Block { get; set; }
        private EStudentStatus StudentStatus { get; set; }


        private enum EStudentStatus
        {
            OnHold,
            Released,
            Accepted,
            Rejected,
            Canceled
        }
    }
}
