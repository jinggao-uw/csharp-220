using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<Models.User>();

            users.Add(new Models.User { Name = "Dave", Password = "hello" });
            users.Add(new Models.User { Name = "Steve", Password = "steve" });
            users.Add(new Models.User { Name = "Lisa", Password = "hello" });

            // Display to the console, all the passwords that are "hello". Hint: Where
            var usersWithHelloPassword =
                from user in users
                where user.Password == "hello"
                select user;

            Console.WriteLine("Display all users' name whose password is \'hello\'");
            foreach (var user in usersWithHelloPassword.ToList())
            {
                Console.WriteLine(user.Name);
            }

            //Delete any passwords that are the lower-cased version of their Name. Do not just look for "steve".
            //It has to be data-driven. Hint: Remove or RemoveAll
            var userPasswordInLowercase =
                from user in users
                where user.Password == user.Name.ToLower()
                select user;

          
            foreach (var user in userPasswordInLowercase.ToList())
            {
                Console.WriteLine("Deleting users with password of lower-cased version of their name: " + user.Name);
                users.Remove(user);
            }

            //Delete the first User that has the password "hello". Hint: First or FirstOrDefault
            Console.WriteLine("Deleting first user with password \"hello\": " + usersWithHelloPassword.First().Name);
            users.Remove(usersWithHelloPassword.First());

            //Display to the console the remaining users with their Name and Password. Here is the only place you can use a for/foreach loop :)
            Console.WriteLine("The remaining user(s) are: ");
            foreach (var user in users)
            {
                Console.WriteLine("User name is: " + user.Name + ", user password is: " + user.Password);
            }
        }
    }
}
