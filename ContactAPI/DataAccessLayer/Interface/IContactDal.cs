using ContactApi.DataAccessLayer.DataObject.Entity;
using ContactApi.DataAccessLayer.DataObject.ViewEntity;

namespace ContactApi.DataAccessLayer.Interface
{
    public interface IContactDal
    {
        Task<ResponseEntity<string>> PostData(ContactEntity modeldata);
        Task<ResponseEntity<ContactViewEntity>> GetAllContact();
        Task<ResponseEntity<bool>> DeleteContact(int ContactID);
        Task<ResponseEntity<string>> Authentication(SigninRequestModel signinRequestModel);
        Task<ResponseEntity<ContactViewEntity>> GetAllContact(string? SortColumn = null, string? SortDirection = null, string? SearchKeyword = null, string? filters = null);
        Task<ResponseEntity<ContactViewEntity>> GetByContactID(int ContactID);

    }
}
