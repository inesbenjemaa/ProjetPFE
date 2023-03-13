using Dapper;
using ProjetPFE.Context;
using ProjetPFE.Contracts;
using ProjetPFE.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjetPFE.Repository
{
    public class EmployeRepository : IEmployeRepository
    {

        private readonly DapperContext _context;

        public EmployeRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<ICollection<employe>> Getemployes()
        {
            var query =  "SELECT employe.employe_id, employe.nom, employe.prenom, employe.matricule, employe.matricule_resp, employe.fonction, " +
            "employe.role, employe.date_recrutement, employe.email, employe.compte_winds, diplome.diplome_id as dipid , " +
            "diplome.nom_diplome, diplome.lieu_diplome, diplome.employe_id, experience.experience_id as expid , experience.poste, " +
            "experience.entreprise, experience.date_debut, experience.date_fin, experience.employe_id, certification.certif_id as certid, " +
            "certification.nom_certif, certification.employe_id, technologie.techno_id as techid, technologie.nom_techno, technologie.employe_id  " +
            "FROM [dbo].[employe] as employe " +
            "LEFT JOIN [dbo].[diplome] as diplome ON employe.employe_id = diplome.employe_id " +
            "LEFT JOIN [dbo].[certification] as certification ON employe.employe_id = certification.employe_id " +
            "LEFT JOIN [dbo].[experience] as experience ON employe.employe_id = experience.employe_id " +
            "LEFT JOIN [dbo].[technologie] as technologie ON employe.employe_id = technologie.employe_id";
           

            var EmployeDictionary = new Dictionary<int, employe>();
            using (var _context = this._context.CreateConnection())
            {
                IEnumerable<employe> result = await _context.QueryAsync<employe, diplome, experience, certification, technologie,  employe >(query, (emp, diplome, experience, certification, technologie) =>
                {
                    employe employe;
                    if (!EmployeDictionary.TryGetValue(emp.employe_id, out employe))
                    {
                        employe = emp;
                        employe.diplomes = new List<diplome>();
                        employe.experiences = new List<experience>();
                        employe.certifications = new List<certification>();
                        employe.technologies = new List<technologie>();
                        EmployeDictionary.Add(employe.employe_id, employe);
                    }
                    if (diplome != null) employe.diplomes.Add(diplome);
                    if (experience != null) employe.experiences.Add(experience);
                    if (certification != null) employe.certifications.Add(certification);
                    if (technologie != null) employe.technologies.Add(technologie);
                    return employe;
                }, splitOn: "dipid, expid, certid, techid");

                return result.ToList();
            }
        }

        public async Task<employe> GetEmployeByIdAsync(int id)
        {
            
                

                var query = @"SELECT employe.employe_id, employe.nom, employe.prenom, employe.matricule, employe.matricule_resp, 
                  employe.fonction, employe.role, employe.date_recrutement, employe.email, employe.compte_winds, 
                  diplome.diplome_id as dipid , diplome.nom_diplome, diplome.lieu_diplome, diplome.employe_id, 
                  experience.experience_id as expid , experience.poste, experience.entreprise, experience.date_debut, 
                  experience.date_fin, experience.employe_id, certification.certif_id as certid, certification.nom_certif, 
                  certification.employe_id, technologie.techno_id as techid, technologie.nom_techno, technologie.employe_id  
                  FROM [dbo].[employe] as employe 
                  LEFT JOIN [dbo].[diplome] as diplome ON employe.employe_id = diplome.employe_id 
                  LEFT JOIN [dbo].[certification] as certification ON employe.employe_id = certification.employe_id 
                  LEFT JOIN [dbo].[experience] as experience ON employe.employe_id = experience.employe_id 
                  LEFT JOIN [dbo].[technologie] as technologie ON employe.employe_id = technologie.employe_id
                  WHERE employe.employe_id = @id";



                using (var _context = this._context.CreateConnection())
                {
                    var employeDict = new Dictionary<int, employe>();
                    var employeList = await _context.QueryAsync<employe, diplome, experience, certification, technologie, employe>(
                        query,
                        (emp, dip, exp, cert, tech) =>
                        {
                            if (!employeDict.TryGetValue(emp.employe_id, out employe employe))
                            {
                                employe = emp;
                                employe.diplomes = new List<diplome>();
                                employe.experiences = new List<experience>();
                                employe.certifications = new List<certification>();
                                employe.technologies = new List<technologie>();
                                employeDict.Add(employe.employe_id, employe);
                            }

                            if (dip != null && !employe.diplomes.Any(d => d.diplome_id == dip.diplome_id))
                            {
                                employe.diplomes.Add(dip);
                            }

                            if (exp != null && !employe.experiences.Any(e => e.experience_id == exp.experience_id))
                            {
                                employe.experiences.Add(exp);
                            }

                            if (cert != null && !employe.certifications.Any(c => c.certif_id == cert.certif_id))
                            {
                                employe.certifications.Add(cert);
                            }

                            if (tech != null && !employe.technologies.Any(t => t.techno_id == tech.techno_id))
                            {
                                employe.technologies.Add(tech);
                            }

                            return employe;
                        },
                        new { id },
                        splitOn: "dipid, expid, certid, techid");

                    return employeList.FirstOrDefault();
                }
            } 


            public async Task<int> AddEmployeAsync(employe employe) { 
               using (var _context = this._context.CreateConnection())

            {
        string query = "INSERT INTO employe(nom, prenom, matricule, matricule_resp, fonction, role, date_recrutement, email, compte_winds) " +
                    "VALUES (@nom, @prenom, @matricule, @matricule_resp, @fonction, @role, @date_recrutement, @email, @compte_winds)";

        int rowsAffected = await _context.ExecuteAsync(query, employe);
        return rowsAffected;
    } }



        public async Task<int> UpdateEmployeAsync(employe employe)
        {
            using (var _context = this._context.CreateConnection())

            {
                string query = "UPDATE employe " +
                    "SET nom = @nom, prenom = @prenom, matricule = @matricule, matricule_resp = @matricule_resp, fonction = @fonction, " +
                    "role = @role, date_recrutement = @date_recrutement, email = @email, compte_winds = @compte_winds " +
                    "WHERE employe_id = @employe_id";

                int rowsAffected = await _context.ExecuteAsync(query, employe);
                return rowsAffected;
            }
        }



        public async Task<int> DeleteEmployeAsync(int id)
        {
            using (var _context = this._context.CreateConnection())


            {
                string query = "DELETE FROM employe WHERE employe_id = @employeId";

                int rowsAffected = await _context.ExecuteAsync(query, new { employeId = id });
                return rowsAffected;
            }
        }

        





    }
}
