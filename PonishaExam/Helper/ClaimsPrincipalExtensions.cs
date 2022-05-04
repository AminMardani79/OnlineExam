using System.Security.Claims;

namespace PonishaExam.Helper
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetStudentId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static string GetStudentName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }
        public static string GetStudentImage(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("StudentImage");
        }
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("Email");
        }
        public static string GetPhoneNumber(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("PhoneNumber");
        }
        public static string GetNationalCode(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("NationalCode");
        }
        public static string GetGrade(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("GradeName");
        }
        public static string GetGradeId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("GradeId");
        }
        public static string GetStudentPassword(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("StudentPassword");
        }
        public static string GetTeacherId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static string GetTeacherName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }
        public static string GetTeacherImage(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("TeacherImage");
        }
        public static string GetTeacherEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("TeacherEmail");
        }
        public static string GetTeacherNationalCode(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("NationalCode");
        }
        public static string GetRoleId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("RoleId");
        }
        public static string GetAdminId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static string GetAdminName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name);
        }
        public static string GetAdminImage(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("AdminImage");
        }
        public static string GetAdminEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("AdminEmail");
        }
        public static string GetAdminNationalCode(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("NationalCode");
        }
    }
}