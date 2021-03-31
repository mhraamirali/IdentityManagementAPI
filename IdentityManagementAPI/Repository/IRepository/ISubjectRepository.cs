using IdentityManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagementAPI.Repository
{
    public interface ISubjectRepository
    {
        ICollection<Subject> GetSubjects();
        Subject GetSubject(int subjectId);
        bool SubjectExists(string name);
        bool SubjectExists(int id);
        bool CreateSubject(Subject subject);
        bool UpdateSubject(Subject subject);
        bool DeleteSubject(Subject subject);
        bool Save();

    }
}
