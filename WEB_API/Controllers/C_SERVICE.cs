using LIB_ANNUAIRE_DAPER;
using Microsoft.AspNetCore.Mvc;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("/Lol")]
    public class C_SERVICE : ControllerBase
    {

        C_BASE _Base;

        public C_SERVICE(C_BASE P_Base)
        {
            _Base = P_Base;
        }

        

        [HttpGet("GetAllPerso", Name = "GetAllPerso")]

        public List<C_PERSONNAGES> Get_All_Personne()
        {
            return _Base.GET_ALL_PERSONNE().ToList();
        }

        [HttpGet("GetAllRole", Name = "GetAllRole")]

        public List<C_ROLES> Get_All_Roles()
        {
            return _Base.Get_All_Roles().ToList();
        }

        [HttpGet("GetPersobyRole", Name = "GetPersobyRole")]

        public List<C_PERSONNAGES> Get_Personnage_by_Roles(int P_ID)
        {
            return _Base.Get_Personnage_by_Roles(P_ID).ToList();
        }

        [HttpPut("Add_Personnage", Name = "AddPerso")]
        public ActionResult<C_PERSONNAGES> Add_Personnage( C_PERSONNAGES P_Perso)
        {
            var Nouveau_Personnage = _Base.Add_Personnage(P_Perso.Id_Roles, P_Perso.Nom_Personnage,P_Perso.Image);

            if (Nouveau_Personnage != false) return Ok(Nouveau_Personnage);
            else return NoContent();
        }

        [HttpPut("Add_Roles", Name = "AddRoles")]
        public ActionResult<C_ROLES> Add_Roles(string P_Nom)
        {
            var Nouveau_Roles = _Base.Add_Roles(P_Nom);

            if (Nouveau_Roles != false) return Ok(Nouveau_Roles);
            else return NoContent();
        }

        [HttpDelete("Delete_Personnages", Name = "DeletePersonnages")]
        public ActionResult Delete_Personnage(int P_Id)
        {
            var Delete_Ok = _Base.Delete_Personnage(P_Id);

            if (Delete_Ok) return Ok();
            else return NotFound(Delete_Ok);
        }

        [HttpDelete("Delete_Roles", Name = "DeleteRoles")]
        public ActionResult Delete_Roles(int P_Id)
        {
            var Delete_Ok = _Base.Delete_Roles(P_Id);

            if (Delete_Ok) return Ok();
            else return NotFound(Delete_Ok);
        }

        [HttpPut("Modif_Perso", Name = "ModifPerso")]
        public ActionResult Modif_Perso([FromBody] C_PERSONNAGES P_Perso)
        {
            var Modif_Ok = _Base.Modif_Perso(P_Perso.Id_Personnages, P_Perso.Id_Roles, P_Perso.Nom_Personnage,P_Perso.Image);

            if (Modif_Ok) return Ok();
            else return NotFound(Modif_Ok);
        }

       
    }

}

