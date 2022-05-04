using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.ViewModels.TestViewModel
{
    public class KeyTestViewModel
    {
        public int QuestionNumber { get; set; }
        public string KeyAnswer { get; set; }
        
    }
}