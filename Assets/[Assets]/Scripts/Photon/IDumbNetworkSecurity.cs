// Barebone network permission checker
public interface IDumbNetworkSecurity
{
    bool CheckPermissionByName(string name, string requirement);
    bool CheckPermissionByID(int id, string requirement);
}