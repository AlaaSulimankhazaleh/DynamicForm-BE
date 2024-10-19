using DynamicFormBuilder.Application.Common;
using DynamicFormBuilder.Application.DTO;
using DynamicFormBuilder.Application.IRepositories;
using DynamicFormBuilder.Application.IServices;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DynamicFormBuilder.Domain.Entities; 

namespace DynamicFormBuilder.Application.Servics
{
    public class FormService : IFormService
    {
        private IRepositoryUnitOfWork _repositoryUnitOfWork;

        public FormService(IRepositoryUnitOfWork repositoryUnitOfWork)
        {
            _repositoryUnitOfWork = repositoryUnitOfWork;
        }

        public async Task<IResponseResult<FormModel>> Add(FormModel model)
        {
            try
            {
                var form = new Form()
                {
                    Id = 0,
                    Name= model.Name,
                    Key = model.Key,
                    UserId =model.UserId,
                    CreatedBy =model.CreatedBy,
                    CreatedDate =model.CreatedDate ,
                    Controls = model.Controls.Select(x => new Control
                    {
                        Id=0,
                        Key=x.Key,
                        Value=x.Value,
                        Label=x.Label,
                        Type=x.Type,
                        Required=x.Required,
                        Order=x.Order,
                        CreatedBy= model.CreatedBy,
                        CreatedDate= model.CreatedDate 

                    }).ToList()

                }; 

                await _repositoryUnitOfWork.Forms.AddAsync(form);
                model.Id = form.Id;
                var controlList = model.Controls.ToList(); // Convert ICollection to List

                for (int i = 0; i < form.Controls.Count; i++)
                {
                    controlList[i].Id = form.Controls.ElementAt(i).Id;
                }
                model.Controls = controlList;
                return new ResponseResult<FormModel>
                {
                    Status = ResultStatus.Success,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult<FormModel>
                {
                    Status = ResultStatus.Failed,
                    Errors = new List<string> { ex.Message ?? "An error occurred while adding the SignUp" },
                    Data = null
                };
            }
        }

        public async Task<IResponseResult<FormModel>> Update(FormModel model)
        {
            try
            {
                var form = new Form()
                {
                    Id = model.Id??0,
                    Name = model.Name,
                    Key = model.Key,
                    UserId = model.UserId,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    ModifiedBy= model.CreatedBy,
                    ModifiedDate=DateTime.Now,
                    Controls = model.Controls.Select(x => new Control
                    {
                        Id = x.Id,
                        Key = x.Key,
                        Value = x.Value,
                        Type = x.Type,
                        Label = x.Label,
                        Required = x.Required,
                        Order = x.Order,
                        CreatedBy = model.CreatedBy,
                        CreatedDate = model.CreatedDate,
                        ModifiedBy = model.CreatedBy,
                        ModifiedDate = DateTime.Now,
                        FormId =model.Id??0

                    }).ToList()

                }; 
                await _repositoryUnitOfWork.Forms.UpdateAsync(form);
                var controlList = model.Controls.ToList();  

                for (int i = 0; i < form.Controls.Count; i++)
                {
                    controlList[i].Id = form.Controls.ElementAt(i).Id;
                }
                model.Controls = controlList;
                return new ResponseResult<FormModel>
                {
                    Status = ResultStatus.Success,
                    Data = model
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult<FormModel>
                {
                    Status = ResultStatus.Failed,
                    Errors = new List<string> { ex.Message ?? "An error occurred while adding the SignUp" },
                    Data = null
                };
            }
        }
    }
}
