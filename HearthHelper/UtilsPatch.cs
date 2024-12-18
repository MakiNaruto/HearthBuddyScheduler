using System;
using System.IO;
using System.Windows;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace HearthHelper;

public class UtilsPatch
{
    public static bool PatchHearthStone(string path, int patchType)
    {
        try
        {
            if (path.Length <= 0)
            {
                return false;
            }
            string text = Path.Combine(path + "/Hearthstone_Data/Plugins/x86", "libacsdk_x86.dll");
            string text2 = Path.Combine(path + "/Hearthstone_Data/Plugins/x86", "libacsdk_x86.dll.back");
            path = Path.Combine(path + "/Hearthstone_Data/Managed", "Assembly-CSharp.dll");
            switch (patchType)
            {
                case 6:
                    if (File.Exists(path + ".bak"))
                    {
                        File.Copy(path + ".bak", path, overwrite: true);
                        File.Delete(path + ".bak");
                    }
                    if (File.Exists(text2))
                    {
                        File.Copy(text2, text, overwrite: true);
                        File.Delete(text2);
                    }
                    break;
                case 1:
                    {
                        string text7 = path;
                        File.Copy(path, path + ".tmp", overwrite: true);
                        DefaultAssemblyResolver defaultAssemblyResolver5 = new DefaultAssemblyResolver();
                        defaultAssemblyResolver5.AddSearchDirectory(text7.Replace("Assembly-CSharp.dll", ""));
                        AssemblyDefinition assemblyDefinition5 = AssemblyDefinition.ReadAssembly(path + ".tmp", new ReaderParameters
                        {
                            AssemblyResolver = defaultAssemblyResolver5
                        });
                        foreach (TypeDefinition type in assemblyDefinition5.MainModule.Types)
                        {
                            if (!(type.Name == "GraphicsResolution"))
                            {
                                continue;
                            }
                            foreach (MethodDefinition method in type.Methods)
                            {
                                if (!(method.Name == "IsAspectRatioWithinLimit"))
                                {
                                    continue;
                                }
                                foreach (Instruction instruction in method.Body.Instructions)
                                {
                                    if (instruction.OpCode.Name == "brfalse.s")
                                    {
                                        instruction.OpCode = OpCodes.Brtrue_S;
                                    }
                                }
                                break;
                            }
                            break;
                        }
                        assemblyDefinition5.Write(path);
                        assemblyDefinition5.Dispose();
                        File.Delete(path + ".tmp");
                        break;
                    }
                case 2:
                    {
                        string text4 = path;
                        File.Copy(path, path + ".tmp", overwrite: true);
                        DefaultAssemblyResolver defaultAssemblyResolver2 = new DefaultAssemblyResolver();
                        defaultAssemblyResolver2.AddSearchDirectory(text4.Replace("Assembly-CSharp.dll", ""));
                        AssemblyDefinition assemblyDefinition2 = AssemblyDefinition.ReadAssembly(path + ".tmp", new ReaderParameters
                        {
                            AssemblyResolver = defaultAssemblyResolver2
                        });
                        foreach (MethodDefinition method2 in assemblyDefinition2.MainModule.GetType("AntiCheatSDK.AntiCheatManager").Methods)
                        {
                            if (method2.Name == "CallInterfaceSetupSDK" || method2.Name == "ClearExtraParams" || method2.Name == "InnerSDKMethodCall" || method2.Name == "OnLoginComplete" || method2.Name == "WriteUserInfo")
                            {
                                MethodBody body;
                                try
                                {
                                    body = method2.Body;
                                }
                                catch
                                {
                                    continue;
                                }
                                body.Instructions.Clear();
                                ILProcessor iLProcessor = body.GetILProcessor();
                                iLProcessor.Append(iLProcessor.Create(OpCodes.Ret));
                            }
                        }
                        assemblyDefinition2.Write(path);
                        assemblyDefinition2.Dispose();
                        File.Delete(path + ".tmp");
                        File.Delete(text);
                        break;
                    }
                case 3:
                    {
                        string text6 = path;
                        File.Copy(path, path + ".tmp", overwrite: true);
                        DefaultAssemblyResolver defaultAssemblyResolver4 = new DefaultAssemblyResolver();
                        defaultAssemblyResolver4.AddSearchDirectory(text6.Replace("Assembly-CSharp.dll", ""));
                        AssemblyDefinition assemblyDefinition4 = AssemblyDefinition.ReadAssembly(path + ".tmp", new ReaderParameters
                        {
                            AssemblyResolver = defaultAssemblyResolver4
                        });
                        foreach (TypeDefinition type2 in assemblyDefinition4.MainModule.Types)
                        {
                            if (!(type2.Name == "ViewCountController"))
                            {
                                continue;
                            }
                            foreach (MethodDefinition method3 in type2.Methods)
                            {
                                if (method3.Name == "GetViewCount")
                                {
                                    method3.Body.Instructions[9].OpCode = OpCodes.Ldc_I4_1;
                                    break;
                                }
                            }
                            break;
                        }
                        assemblyDefinition4.Write(path);
                        assemblyDefinition4.Dispose();
                        File.Delete(path + ".tmp");
                        break;
                    }
                case 4:
                    {
                        string text5 = path;
                        File.Copy(path, path + ".tmp", overwrite: true);
                        DefaultAssemblyResolver defaultAssemblyResolver3 = new DefaultAssemblyResolver();
                        defaultAssemblyResolver3.AddSearchDirectory(text5.Replace("Assembly-CSharp.dll", ""));
                        AssemblyDefinition assemblyDefinition3 = AssemblyDefinition.ReadAssembly(path + ".tmp", new ReaderParameters
                        {
                            AssemblyResolver = defaultAssemblyResolver3
                        });
                        foreach (TypeDefinition type3 in assemblyDefinition3.MainModule.Types)
                        {
                            if (!(type3.Name == "SplashScreen"))
                            {
                                continue;
                            }
                            foreach (MethodDefinition method4 in type3.Methods)
                            {
                                if (method4.Name == "GetRatingsScreenRegion")
                                {
                                    method4.Body.Instructions[0].OpCode = OpCodes.Ret;
                                    break;
                                }
                            }
                            break;
                        }
                        assemblyDefinition3.Write(path);
                        assemblyDefinition3.Dispose();
                        File.Delete(path + ".tmp");
                        break;
                    }
                case 5:
                    {
                        string text3 = path;
                        File.Copy(path, path + ".tmp", overwrite: true);
                        DefaultAssemblyResolver defaultAssemblyResolver = new DefaultAssemblyResolver();
                        defaultAssemblyResolver.AddSearchDirectory(text3.Replace("Assembly-CSharp.dll", ""));
                        AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(path + ".tmp", new ReaderParameters
                        {
                            AssemblyResolver = defaultAssemblyResolver
                        });
                        foreach (TypeDefinition type4 in assemblyDefinition.MainModule.Types)
                        {
                            if (!(type4.Name == "Entity"))
                            {
                                continue;
                            }
                            foreach (MethodDefinition method5 in type4.Methods)
                            {
                                if (method5.Name == "GetPremiumType")
                                {
                                    method5.Body.Instructions[11].OpCode = OpCodes.Ldc_I4_0;
                                    method5.Body.Instructions[13].OpCode = OpCodes.Ldc_I4_0;
                                    break;
                                }
                            }
                            break;
                        }
                        assemblyDefinition.Write(path);
                        assemblyDefinition.Dispose();
                        File.Delete(path + ".tmp");
                        break;
                    }
                case 0:
                    File.Copy(path, path + ".bak", overwrite: true);
                    File.Copy(text, text2, overwrite: true);
                    break;
            }
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "", MessageBoxButton.OK);
            return false;
        }
    }
}
