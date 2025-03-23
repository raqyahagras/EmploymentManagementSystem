namespace EmploymentManagementSystem.API.Helper;

using System;
using System.Linq.Expressions;

public static class ExpressionCombiner
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        if (first == null) return second;
        if (second == null) return first;

        var parameter = Expression.Parameter(typeof(T));

        var combined = Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(
                Expression.Invoke(first, parameter),
                Expression.Invoke(second, parameter)
            ), parameter);

        return combined;
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        if (first == null) return second;
        if (second == null) return first;

        var parameter = Expression.Parameter(typeof(T));

        var combined = Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(
                Expression.Invoke(first, parameter),
                Expression.Invoke(second, parameter)
            ), parameter);

        return combined;
    }
} 

