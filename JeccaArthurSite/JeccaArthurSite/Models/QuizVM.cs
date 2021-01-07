using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Winterfell.Models;

namespace Winterfell.Models
{
    public class QuizVM
    {
        // User's answer to the first question
        public String answer1 { get; set; }
        // Result of checking the answer
        public String result1 { get; set; }

        // Second question results
        public String answer2 { get; set; }
        public String result2 { get; set; }

        // Thirds question results
        public String answer3 { get; set; }
        public String result3 { get; set; }

        // Checks each answer to see if it's right or wrong
        // Returns "Right" or "Wrong"
        public void CheckAnswers()
        {
            result1 = answer1 == "Direwolf" ? "Right" : "Wrong";
            result2 = answer2 == "Winter is coming" ? "Right" : "Wrong";
            result3 = answer3 == "false" ? "Right" : "Wrong";

        }
    }
}
