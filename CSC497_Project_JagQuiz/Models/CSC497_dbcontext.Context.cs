﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSC497_Project_JagQuiz.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Project_CSC497Entities : DbContext
    {
        public Project_CSC497Entities()
            : base("name=Project_CSC497Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int uspAddCourse(string pAccountEmail, string pCourse)
        {
            var pAccountEmailParameter = pAccountEmail != null ?
                new ObjectParameter("pAccountEmail", pAccountEmail) :
                new ObjectParameter("pAccountEmail", typeof(string));
    
            var pCourseParameter = pCourse != null ?
                new ObjectParameter("pCourse", pCourse) :
                new ObjectParameter("pCourse", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspAddCourse", pAccountEmailParameter, pCourseParameter);
        }
    
        public virtual int uspAddTerm(string pTerm, string pDef, string pModule, string pCourseDescript)
        {
            var pTermParameter = pTerm != null ?
                new ObjectParameter("pTerm", pTerm) :
                new ObjectParameter("pTerm", typeof(string));
    
            var pDefParameter = pDef != null ?
                new ObjectParameter("pDef", pDef) :
                new ObjectParameter("pDef", typeof(string));
    
            var pModuleParameter = pModule != null ?
                new ObjectParameter("pModule", pModule) :
                new ObjectParameter("pModule", typeof(string));
    
            var pCourseDescriptParameter = pCourseDescript != null ?
                new ObjectParameter("pCourseDescript", pCourseDescript) :
                new ObjectParameter("pCourseDescript", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspAddTerm", pTermParameter, pDefParameter, pModuleParameter, pCourseDescriptParameter);
        }
    
        public virtual int uspAddToCourse(string pEmail, string pCourseDscpt)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            var pCourseDscptParameter = pCourseDscpt != null ?
                new ObjectParameter("pCourseDscpt", pCourseDscpt) :
                new ObjectParameter("pCourseDscpt", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspAddToCourse", pEmailParameter, pCourseDscptParameter);
        }
    
        public virtual int uspChangePassword(string pEmail, string pNewPassword)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            var pNewPasswordParameter = pNewPassword != null ?
                new ObjectParameter("pNewPassword", pNewPassword) :
                new ObjectParameter("pNewPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspChangePassword", pEmailParameter, pNewPasswordParameter);
        }
    
        public virtual int uspDeleteAccount(string pEmail)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspDeleteAccount", pEmailParameter);
        }
    
        public virtual int uspDeleteCourse(string pCourseDescript)
        {
            var pCourseDescriptParameter = pCourseDescript != null ?
                new ObjectParameter("pCourseDescript", pCourseDescript) :
                new ObjectParameter("pCourseDescript", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspDeleteCourse", pCourseDescriptParameter);
        }
    
        public virtual int uspDeleteUser(string pEmail)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspDeleteUser", pEmailParameter);
        }
    
        public virtual ObjectResult<string> uspGetAllCourses()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("uspGetAllCourses");
        }
    
        public virtual ObjectResult<string> uspGetCourseByUser(Nullable<int> pAccountFK)
        {
            var pAccountFKParameter = pAccountFK.HasValue ?
                new ObjectParameter("pAccountFK", pAccountFK) :
                new ObjectParameter("pAccountFK", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("uspGetCourseByUser", pAccountFKParameter);
        }
    
        public virtual ObjectResult<string> uspGetCoursesAsAdmin(string pEmail)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("uspGetCoursesAsAdmin", pEmailParameter);
        }
    
        public virtual ObjectResult<string> uspGetCouseAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("uspGetCouseAll");
        }
    
        public virtual ObjectResult<uspGetStudents_Result> uspGetStudents(string pCourseID)
        {
            var pCourseIDParameter = pCourseID != null ?
                new ObjectParameter("pCourseID", pCourseID) :
                new ObjectParameter("pCourseID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<uspGetStudents_Result>("uspGetStudents", pCourseIDParameter);
        }
    
        public virtual ObjectResult<uspGetTerm_Result> uspGetTerm(string pTerm)
        {
            var pTermParameter = pTerm != null ?
                new ObjectParameter("pTerm", pTerm) :
                new ObjectParameter("pTerm", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<uspGetTerm_Result>("uspGetTerm", pTermParameter);
        }
    
        public virtual ObjectResult<uspGetTermByCourse_Result> uspGetTermByCourse(string pCourse)
        {
            var pCourseParameter = pCourse != null ?
                new ObjectParameter("pCourse", pCourse) :
                new ObjectParameter("pCourse", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<uspGetTermByCourse_Result>("uspGetTermByCourse", pCourseParameter);
        }
    
        public virtual ObjectResult<uspGetTermsByUser_Result> uspGetTermsByUser(string pEmail)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<uspGetTermsByUser_Result>("uspGetTermsByUser", pEmailParameter);
        }
    
        public virtual ObjectResult<string> uspRegisterUser(string pEmail, string pPasswordHash, string pFirstName, string pLastName, Nullable<int> pAccountType)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            var pPasswordHashParameter = pPasswordHash != null ?
                new ObjectParameter("pPasswordHash", pPasswordHash) :
                new ObjectParameter("pPasswordHash", typeof(string));
    
            var pFirstNameParameter = pFirstName != null ?
                new ObjectParameter("pFirstName", pFirstName) :
                new ObjectParameter("pFirstName", typeof(string));
    
            var pLastNameParameter = pLastName != null ?
                new ObjectParameter("pLastName", pLastName) :
                new ObjectParameter("pLastName", typeof(string));
    
            var pAccountTypeParameter = pAccountType.HasValue ?
                new ObjectParameter("pAccountType", pAccountType) :
                new ObjectParameter("pAccountType", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("uspRegisterUser", pEmailParameter, pPasswordHashParameter, pFirstNameParameter, pLastNameParameter, pAccountTypeParameter);
        }
    
        public virtual int uspRemoveCourse(string pEmail, string pCourseDscpt)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            var pCourseDscptParameter = pCourseDscpt != null ?
                new ObjectParameter("pCourseDscpt", pCourseDscpt) :
                new ObjectParameter("pCourseDscpt", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspRemoveCourse", pEmailParameter, pCourseDscptParameter);
        }
    
        public virtual int uspUpdateCourse(string pOldCourse, string pNewDescript)
        {
            var pOldCourseParameter = pOldCourse != null ?
                new ObjectParameter("pOldCourse", pOldCourse) :
                new ObjectParameter("pOldCourse", typeof(string));
    
            var pNewDescriptParameter = pNewDescript != null ?
                new ObjectParameter("pNewDescript", pNewDescript) :
                new ObjectParameter("pNewDescript", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspUpdateCourse", pOldCourseParameter, pNewDescriptParameter);
        }
    
        public virtual int uspUpdateTerm(string pTerm, string pDef)
        {
            var pTermParameter = pTerm != null ?
                new ObjectParameter("pTerm", pTerm) :
                new ObjectParameter("pTerm", typeof(string));
    
            var pDefParameter = pDef != null ?
                new ObjectParameter("pDef", pDef) :
                new ObjectParameter("pDef", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("uspUpdateTerm", pTermParameter, pDefParameter);
        }
    
        public virtual ObjectResult<uspLogIn_Result> uspLogIn(string pEmail, string pPassword)
        {
            var pEmailParameter = pEmail != null ?
                new ObjectParameter("pEmail", pEmail) :
                new ObjectParameter("pEmail", typeof(string));
    
            var pPasswordParameter = pPassword != null ?
                new ObjectParameter("pPassword", pPassword) :
                new ObjectParameter("pPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<uspLogIn_Result>("uspLogIn", pEmailParameter, pPasswordParameter);
        }
    
        public virtual ObjectResult<uspGetTermsByModule_Result> uspGetTermsByModule(string pCourse, string pModule)
        {
            var pCourseParameter = pCourse != null ?
                new ObjectParameter("pCourse", pCourse) :
                new ObjectParameter("pCourse", typeof(string));
    
            var pModuleParameter = pModule != null ?
                new ObjectParameter("pModule", pModule) :
                new ObjectParameter("pModule", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<uspGetTermsByModule_Result>("uspGetTermsByModule", pCourseParameter, pModuleParameter);
        }
    }
}
