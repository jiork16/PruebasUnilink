using APIUnilink.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace APIUnilink.Controllers
{

    [Route("api/game")]
    [ApiController]
    public class GameController : Controller
    {
        UNILINKContext Db = new UNILINKContext();
        // POST: GameController/Delete/
        private NumColor GameNumColor()
        {
            var randomNumber = new Random().Next(0, 37);
            var randColor = new Random().Next(0, 2);
            NumColor result = new()
            {
                Color = randColor == 1 ? "Rojo" : "Negro",
                Number = randomNumber,
                Is_pair = randomNumber % 2 == 0
            };
            return result;
        }
        private decimal ResultBet(NumColor resltGame, BetUser bet)
        {
            if (resltGame.Number== bet.Number && bet.Color == resltGame.Color)
            {
                return (decimal)(bet.Amount * 3);
            }
            if ((bet.Number %2==0) == resltGame.Is_pair && bet.Color == resltGame.Color)
            {
                return (decimal)(bet.Amount);
            }
            if (bet.Color == resltGame.Color)
            {
                return (decimal)(bet.Amount / 2);
            }
            return (decimal)(bet.Amount * -1);
        }
        // POST: GameController/Delete/5
        [HttpPost("rule")]
        public NumColor Post(BetUser bet)
        {
            try
            {
                NumColor result_game = GameNumColor();
                decimal resultBet = ResultBet(result_game, bet);
                result_game.Gain = resultBet;
                if (bet.Is_Save)
                {
                    var user = Db.Users.Where(p => p.Username.ToUpper() == bet.Username.ToUpper()).FirstOrDefault();
                    if (user == null)
                    {
                        user = new User();
                        user.Amount = bet.Initial_Balance + result_game.Gain;
                        user.Username = bet.Username.ToUpper();
                        Db.Users.Add(user);
                        Db.SaveChanges();
                    }
                    else
                    {
                        user.Amount = user.Amount + result_game.Gain;
                        Db.Users.Update(user);
                        Db.SaveChanges();
                    }
                }
                return  result_game;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
