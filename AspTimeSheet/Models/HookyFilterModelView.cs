using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspTimeSheet.Data;
using Microsoft.EntityFrameworkCore.Query;

namespace AspTimeSheet.Models
{
    public enum StringFilter
    {
        [Display(Name = "Начинается с")]
        Begins,
        [Display(Name = "Содержит")]
        Contains,
        [Display(Name = "Равно")]
        Equal
    };

    public enum DateFilter
    {
        [Display(Name = "Меньше")]
        Less,
        [Display(Name = "Меньше или равно")]
        LessOrEqual,
        [Display(Name = "Равно")]
        Equal,
        [Display(Name = "Больше или равно")]
        GreatOrEqual,
        [Display(Name = "Больше")]
        Great
    };

    public abstract class FilterModel<ModelType>
    {
        public abstract Func<ModelType, bool> FilterExpression { get; }
    }

    public class HookyFilterModelView 
    {
        public StringFilter PositionOperation { get; set; }
        [Display(Name = "Должность")]
        public string PositionName { get; set; }

        public StringFilter PersonNameOperation { get; set; }
        [Display(Name = "Имя")]
        public string PersonName { get; set; }

        public StringFilter PersonMiddleNameOperation { get; set; }
        [Display(Name = "Отчество")]
        public string PersonMiddleName { get; set; }

        public StringFilter PersonLastNameOperation { get; set; }
        [Display(Name = "Фамилия")]
        public string PersonLastName { get; set; }

        public StringFilter CommentOperation { get; set; }
        [Display(Name = "Причина отстутствия")]
        public string Comment { get; set; }

        public DateFilter HookyDateOperation { get; set; }
        [Display(Name = "Дата отсутствия")]
        [DataType(DataType.Date)]
        public DateTime? HookyDate { get; set; }

        public DateFilter HookyTimeOperation { get; set; }
        [Display(Name = "Время отстутствия")]
        [DataType(DataType.Time)]
        public TimeSpan ? HookyTime { get; set; }

        internal IQueryable<Hooky> Apply(IQueryable<Hooky> hookData)
        {
            if (!string.IsNullOrEmpty(PositionName))
            {
                switch (PositionOperation)
                {
                    case StringFilter.Begins:
                        hookData = hookData.Where(x => x.Position.Name.StartsWith(PositionName));
                        break;
                    case StringFilter.Contains:
                        hookData = hookData.Where(x => x.Position.Name.Contains(PositionName));
                        break;
                    case StringFilter.Equal:
                        hookData = hookData.Where(x => x.Position.Name == PositionName);
                        break;
                }

            }
            if (!string.IsNullOrEmpty(PersonName))
            {
                switch (PersonNameOperation)
                {
                    case StringFilter.Begins:
                        hookData = hookData.Where(x => x.Person.Name.StartsWith(PersonName));
                        break;
                    case StringFilter.Contains:
                        hookData = hookData.Where(x => x.Person.Name.Contains(PersonName));
                        break;
                    case StringFilter.Equal:
                        hookData = hookData.Where(x => x.Person.Name == PersonName);
                        break;
                }

            }

            if (!string.IsNullOrEmpty(PersonMiddleName))
            {
                switch (PersonMiddleNameOperation)
                {
                    case StringFilter.Begins:
                        hookData = hookData.Where(x => x.Person.MiddleName.StartsWith(PersonMiddleName));
                        break;
                    case StringFilter.Contains:
                        hookData = hookData.Where(x => x.Person.MiddleName.Contains(PersonMiddleName));
                        break;
                    case StringFilter.Equal:
                        hookData = hookData.Where(x => x.Person.MiddleName == PersonMiddleName);
                        break;
                }

            }

            if (!string.IsNullOrEmpty(PersonLastName))
            {
                switch (PersonLastNameOperation)
                {
                    case StringFilter.Begins:
                        hookData = hookData.Where(x => x.Person.LastName.StartsWith(PersonLastName));
                        break;
                    case StringFilter.Contains:
                        hookData = hookData.Where(x => x.Person.LastName.Contains(PersonLastName));
                        break;
                    case StringFilter.Equal:
                        hookData = hookData.Where(x => x.Person.LastName == PersonLastName);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(Comment))
            {
                switch (CommentOperation)
                {
                    case StringFilter.Begins:
                        hookData = hookData.Where(x => x.Comment.StartsWith(Comment));
                        break;
                    case StringFilter.Contains:
                        hookData = hookData.Where(x => x.Comment.Contains(Comment));
                        break;
                    case StringFilter.Equal:
                        hookData = hookData.Where(x => x.Comment == Comment);
                        break;
                }
            }

            if (HookyDate != null)
            {
                switch (HookyDateOperation)
                {
                    case DateFilter.Equal:
                        hookData = hookData.Where(x => x.HookyDate == HookyDate);
                        break;
                    case DateFilter.Great:
                        hookData = hookData.Where(x => x.HookyDate > HookyDate);
                        break;
                    case DateFilter.GreatOrEqual:
                        hookData = hookData.Where(x => x.HookyDate >= HookyDate);
                        break;
                    case DateFilter.Less:
                        hookData = hookData.Where(x => x.HookyDate < HookyDate);
                        break;
                    case DateFilter.LessOrEqual:
                        hookData = hookData.Where(x => x.HookyDate <= HookyDate);
                        break;
                }
            }

            if (HookyTime != null)
            {
                switch (HookyDateOperation)
                {
                    case DateFilter.Equal:
                        hookData = hookData.Where(x => x.HookyTime == HookyTime);
                        break;
                    case DateFilter.Great:
                        hookData = hookData.Where(x => x.HookyTime > HookyTime);
                        break;
                    case DateFilter.GreatOrEqual:
                        hookData = hookData.Where(x => x.HookyTime >= HookyTime);
                        break;
                    case DateFilter.Less:
                        hookData = hookData.Where(x => x.HookyTime < HookyTime);
                        break;
                    case DateFilter.LessOrEqual:
                        hookData = hookData.Where(x => x.HookyTime <= HookyTime);
                        break;
                }
            }

            return hookData;
        }

    }
}
