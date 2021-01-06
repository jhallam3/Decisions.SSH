using DecisionsFramework.Design.Flow;
using Renci.SshNet;
using System;

namespace Decisions.SSH
{
    [AutoRegisterMethodsOnClass(true, "Integration/SSH")]
    public class SSHSteps
    {
        public bool TryConnect(ConnectionInfo connectionInfo)
        {
            try
            {
                
                using (var client = new SshClient(connectionInfo))
                {
                    client.Connect();
                    return client.IsConnected;
                }
            }
            catch (Exception ex2)
            {

                return false;
            }
        }


        public ConnectionInfo CreateConnectionInfo(string host, string username, string password)
        {
            return new ConnectionInfo(host, username,
                                              new PasswordAuthenticationMethod(username, password));
        }
       



        public string RunCommand(string host, string username, string password, string command)
        {
            try
            {
                using (var client = new SshClient(new ConnectionInfo(host, username, new PasswordAuthenticationMethod(username, password))))
                {
                    client.Connect();
                    client.RunCommand(command);
                    return "done";
                }
            }
            catch (Exception ex2)
            {

                throw;
            }
        }

   

    }
}
