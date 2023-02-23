using lesson1.Models;

namespace lesson1.Interfaces;

     public interface jobInterface
    {
    
       public List<Job> GetAll() ;

       public Job Get(int id) ;

       public void Add(Job Job);
       
       public bool Update(int id, Job newJob);

       public  bool Delete(int id);
    }

