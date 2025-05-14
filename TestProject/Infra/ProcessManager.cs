using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TestProject.Infra;
public class ProcessManager
{
    public static string ExecuteCommand(string file, string arguments)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = FindExecutable(file),
            Arguments = arguments,
            UseShellExecute = false,
            WorkingDirectory = string.Empty,
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardOutput = true
        };

        Process? process = Process.Start(processStartInfo);

        if (process == null)
            throw new Exception($"Falha ao iniciar processo: {file} {arguments}");

        process.WaitForExit();

        if (process.ExitCode == 0)
            return process.StandardOutput.ReadToEnd();

        return string.Empty;
    }

    public static Process StartProccess(string file, string arguments, string workingDirectory,
        bool useShell = true)
    {
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = FindExecutable(file),
            Arguments = arguments,
            UseShellExecute = useShell,
            WorkingDirectory = workingDirectory,
            WindowStyle = ProcessWindowStyle.Hidden
        };

        Process? process = Process.Start(processStartInfo);
        return process!;
    }

    private static string FindExecutable(string name) =>
        Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator)
            .Select(p => Path.Combine(p, name))
            .Select(p =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? new[] { $"{p}.cmd", $"{p}.exe" }
                    : new[] { p })
            .SelectMany(a => a)
            .FirstOrDefault(File.Exists) ??
        throw new FileNotFoundException("Could not find executable.", name);

    public static void StopProcess(Process processToStop)
    {
        if (processToStop == null || processToStop.Id == 0)
            return;

        if (!processToStop.HasExited)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                KillWindowsProcess(processToStop.Id);
            }
            else
            {
                KillUnixProcess(processToStop.Id);
            }
        }

        processToStop.Dispose();
    }

    private static void KillWindowsProcess(int processId)
    {
        using Process? killer =
            Process.Start(
                new ProcessStartInfo("taskkill.exe", $"/PID {processId} /T /F")
                {
                    UseShellExecute = false
                });
        killer?.WaitForExit(2000);
    }

    private static void KillUnixProcess(int processId)
    {
        using (Process? idGetter =
            Process.Start(new ProcessStartInfo("ps", $"-o pid= --ppid {processId}")
            {
                UseShellExecute = false,
                RedirectStandardOutput = true
            }))
        {
            bool exited = idGetter != null && idGetter.WaitForExit(2000);
            if (exited && idGetter!.ExitCode == 0)
            {
                string stdout = idGetter.StandardOutput.ReadToEnd();

                List<int> pids = stdout.Split("\n")
                    .Where(pid => !string.IsNullOrEmpty(pid))
                    .Select(int.Parse)
                    .ToList();
                foreach (int pid in pids)
                    KillUnixProcess(pid);
            }
        }

        Process.GetProcessById(processId).Kill();
    }
}
