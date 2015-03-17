using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using System.Web.Security;

namespace Bandits.PlayerManagement
{
    public static class PMFunctions
    {
        public static Player NewPlayer()
        {
            Player player = new Player();
            player.Person = new Person();
            player.CreateDate = DateTime.Now;
            player.Person.CreateDate = DateTime.Now;
            //player.Person.CreateUser = Membership.FindUsersByName(Cu)
            return player;
        }
    }
}