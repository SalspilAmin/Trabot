using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Behaviors
{

    public class ValidateBehavior<TRequest, TResopnse> : IPipelineBehavior<TRequest, TResopnse> where TRequest : IRequest<TResopnse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;
        private readonly LocalizationService Localize;

        public ValidateBehavior(IEnumerable<IValidator<TRequest>> validators, LocalizationService localize)
        {
            this.validators = validators;
            Localize = localize;
        }

        public async Task<TResopnse> Handle(TRequest request, RequestHandlerDelegate<TResopnse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new  ValidationContext<TRequest>(request);
                var validationResult = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
                var failures = validationResult.SelectMany(x => x.Errors).Where(x => x != null).ToList();
                  
                if (failures.Count != 0)
                {
                    var errorsmessages = failures
                            .Select(x =>
                        $"{Localize.Get(x.PropertyName)} : {Localize.Get(x.ErrorCode ?? x.ErrorMessage)}"
                                     )
                                     .ToList();

                    throw new CustomValidationExeption(errorsmessages);
                }
                
            
            }
            return  await next();

        }
    }
}
