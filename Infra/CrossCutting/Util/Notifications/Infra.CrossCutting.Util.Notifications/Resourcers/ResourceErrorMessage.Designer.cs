﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infra.CrossCutting.Util.Notifications.Resourcers {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ResourceErrorMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceErrorMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Infra.CrossCutting.Util.Notifications.Resourcers.ResourceErrorMessage", typeof(ResourceErrorMessage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A data não deverá ser o menor valor.
        /// </summary>
        public static string DATA_MINIMA {
            get {
                return ResourceManager.GetString("DATA_MINIMA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A data é obrigatória.
        /// </summary>
        public static string DATA_OBRIGATORIA {
            get {
                return ResourceManager.GetString("DATA_OBRIGATORIA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O email já está cadastrado.
        /// </summary>
        public static string EMAIL_CADASTRADO {
            get {
                return ResourceManager.GetString("EMAIL_CADASTRADO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O campo de email não pode ser vazio.
        /// </summary>
        public static string EMAIL_VAZIO {
            get {
                return ResourceManager.GetString("EMAIL_VAZIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Falha na autenticação.
        /// </summary>
        public static string FALHA_AO_GERAR_TOKEN {
            get {
                return ResourceManager.GetString("FALHA_AO_GERAR_TOKEN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Erro ao inserir no banco de dados.
        /// </summary>
        public static string FALHA_NO_COMMIT {
            get {
                return ResourceManager.GetString("FALHA_NO_COMMIT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O formato do email é invalido.
        /// </summary>
        public static string FORMATO_EMAIL_INVALIDO {
            get {
                return ResourceManager.GetString("FORMATO_EMAIL_INVALIDO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O campo de nome não pode ser vazio.
        /// </summary>
        public static string NOME_VAZIO {
            get {
                return ResourceManager.GetString("NOME_VAZIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A senha informada é invalida.
        /// </summary>
        public static string SENHA_INVALIDA {
            get {
                return ResourceManager.GetString("SENHA_INVALIDA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O campo de senha não pode ser vazio.
        /// </summary>
        public static string SENHA_VAZIA {
            get {
                return ResourceManager.GetString("SENHA_VAZIA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to O usuário não foi encontrado na base de dados.
        /// </summary>
        public static string USUARIO_NAO_ENCONTRADO {
            get {
                return ResourceManager.GetString("USUARIO_NAO_ENCONTRADO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id do usuário não pode ser vazio.
        /// </summary>
        public static string USUARIO_VAZIO {
            get {
                return ResourceManager.GetString("USUARIO_VAZIO", resourceCulture);
            }
        }
    }
}
