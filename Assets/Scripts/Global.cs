public static class Global
{   
    public static int NumberOfDisks = 1;
    public static float DiskVelocity = 0.1f;
    public static float RadiusDiff = 1f;
    public static float MinRadius = 0.5f;
    public static float DiskBaseDiff = 1.5f;
    public static float DistanceBetweenBases = 2f + DiskBaseDiff;
    public static bool DiskMoving = false;

    public static float DiskHeight() { return 0.2f * NumberOfDisks / 2; }
    public static float DiskRaiseHeight() { return DiskHeight() * 2 * (NumberOfDisks + 1) + NumberOfDisks; }
}
