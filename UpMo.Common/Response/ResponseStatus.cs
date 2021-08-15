namespace UpMo.Common.Response
{
    public enum ResponseStatus : int
    {
        OK = 200,
        Created = 201,
        NoContent = 204,

        BadRequest = 400,
        Forbid = 403,
        NotFound = 404,

        Internal = 500
    }
}