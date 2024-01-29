using LibraryManagementSystem.Data;

namespace LibraryManagementSystem;

public static class Memory
{
    public static AccessLevel UserAccess { get; set; } = AccessLevel.Unconfirmed;
    public static string? UserName { get; set; }
    public static bool ProgramLoop { get; set; } = true;
}