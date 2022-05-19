using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Jornal
{
    internal class wModel
    {
    }
    public class Lesson_Mark
    {
        public int IdSt { get; set; }
        public int IdTch { get; set; }
        public int IdPrd { get; set; }
        public DateTime DateLM { get; set; }

        public Lesson_Mark(int idst, int idtch, int idprd, DateTime datelm)
        {
            this.IdSt = idst;
            this.IdTch = idtch;
            this.IdPrd = idprd;
            this.DateLM = datelm;
        }
    }
    public class L_M
    {
        public L_M(int IdSt, string Name)
        {
            this.IdSt = IdSt;
            this.Name = Name;
        }
        public int IdSt { get; set; }
        public string Name { get; set; }
    }
}
   
