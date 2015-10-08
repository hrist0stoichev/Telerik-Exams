namespace Application.WebServices.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class CreateGameModel
    {
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }
    }
}