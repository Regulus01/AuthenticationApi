using System.ComponentModel.DataAnnotations;
using Infra.CrossCutting.Util.Notifications.Resourcers;
using Microsoft.AspNetCore.Mvc;

namespace Application.Authorization.Dto;

public class InserirUltimoLoginDto
{
    /// <summary>
    /// Data do ultimo login do usuário
    /// </summary>
    [Required(ErrorMessageResourceType = typeof(ResourceErrorMessage),
              ErrorMessageResourceName = nameof(ResourceErrorMessage.DATA_OBRIGATORIA))]
    [FromBody]
    public DateTimeOffset DataDoUltimoLogin { get; set; }
}