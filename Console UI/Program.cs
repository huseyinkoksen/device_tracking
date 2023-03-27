// See https://aka.ms/new-console-template for more information

using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities;

IHairDresserDal dal = new EfHairDresserDal();
HairDresserManager manager = new HairDresserManager(dal);
ICommentDal commentDal = new EfCommentDal();
CommentManager commentManager = new CommentManager(commentDal);


Console.WriteLine(manager.GetAll().Data.Equals(1));
Console.WriteLine(commentManager.GetAll().Data.First().Content);
