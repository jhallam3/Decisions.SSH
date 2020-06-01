using DecisionsFramework.Design.Flow;

using Rebex.Net;
using Rebex.TerminalEmulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The simplest types of steps are method based sync steps.  Simply write whatever
/// .NET code you want and use an attribute on the CLASS or on the METHOD itself to 
/// register that code with the workflow engine as a flow step.
/// </summary>
namespace SSH.Steps
{
    [AutoRegisterMethodsOnClass(true, "Integration/SSH")]
    [AutoRegisterAgentFlowElementFactory("monkey")]
    public class SimpleStepCode
    {
        public string TryConnect(string host, string username, string password)
        {
            try
            {
                using (var ssh = new Ssh())
                {
                    // connect and log in
                    ssh.Connect(host);
                    ssh.Login(username, password);

                    // execute a simple command
                    var response = ssh.RunCommand("echo Hello world!");

                    // display the response
                    return response;
                }
            }
            catch (Exception ex2)
            {
                
                return ex2.Message;
            }
        }

        public bool TryConnectTrueFalse(string host, string username, string password)
        {
            try
            {
                var tc = TryConnect(host, username, password);
                {
                    if (tc.StartsWith("Hello world"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string RunCommand (string host, string username, string password, string command)
        {
            try
            {
                using (var ssh = new Ssh())
                {
                    // connect and log in
                    ssh.Connect(host);
                    ssh.Login(username, password);

                    // execute a simple command
                    var response = ssh.RunCommand(command);

                    // display the response
                    return response;
                }
            }
            catch (Exception ex2)
            {

                return ex2.Message;
            }
        }

        public string RunCommands(string host, string username, string password, string[] commands)
        {
            string response ="";
            try
            {
                
                using (var ssh = new Ssh())
                {
                    // connect and log in
                    ssh.Connect(host);
                    ssh.Login(username, password);

                    Scripting scripting = ssh.StartScripting();
                    ssh.LogWriter = new Rebex.FileLogWriter(@"c:\rebexlog\log.txt", Rebex.LogLevel.Verbose);
                    // automatically detect remote prompt
                    scripting.KeepAlive();


                    foreach (var item in commands)
                    {
                        scripting.SendCommand(item);

                        //response = scripting.ReadUntilPrompt();
                        //string line = @"C:\Vagrant\buildme>";
                        scripting.WaitFor(ScriptEvent.AnyText);
                        string promptLine = "";

                        while (true)
                        {
                            string line = scripting.ReadUntil(ScriptEvent.FromRegex("[>#]") & ScriptEvent.Delay(30000));

                            line = line.TrimStart();

                            if (line != promptLine)
                            {
                                scripting.Send(FunctionKey.Enter);
                                promptLine = line;
                            }
                            else
                            {
                                scripting.Prompt = "string:" + promptLine;
                                break;
                            }
                        }

                    }
                    

                    // read its response
                  
                    // display the response
                    return response;
                }
            }
            catch (Exception ex2)
            {

                return ex2.Message;
            }
        }

    }
}
