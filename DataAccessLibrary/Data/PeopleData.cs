using DataAccessLibrary.Models;
using System.Threading.Tasks.Dataflow;

namespace DataAccessLibrary.Data;

public class PeopleData : IPeopleData
{
    private readonly ISqlDataAccess _sql;

    public PeopleData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public async Task<IEnumerable<PersonModel>> GetAllPeople()
    {
        var output = await _sql.LoadDataAsync<PersonModel, dynamic>(
            "dbo.spPeople_GetAll",
            new { } //erstellt ein neuen Objekt
            // hier hätte ein connectionstring noch hinzugefügt werden können, da es nicht deklariert wird
            //wird es als "Default" deklariert.
            );
        return output;
    }

    public async Task UpdatePerson(PersonModel person)
    {
        await _sql.SaveDataAsync("dbo.spPeople_Update", person);
    }

    public async Task InsertPerson(PersonModel person)
    {
        await _sql.SaveDataAsync("dbo.spPeople_Insert",
            new { person.FirstName, person.LastName});
    }

    public async Task DeletePerson(int id)
    {
        await _sql.SaveDataAsync("dbo.spPeople_Delete",
            new { Id = id });
    }
}
