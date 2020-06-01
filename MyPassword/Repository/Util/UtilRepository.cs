using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;

namespace MyPassword.Repository.Util
{
    public static class UtilRepository
    {
        /// <summary>
        ///	<para>Gera um registro pronto para ser alterado ou incluido no banco quando ocorrer o submit.</para>
        /// <para>Obs 1:</para>																																												
        /// <para>Se usado com uma busca do tipo x=>x.&lt;colunapk int32&gt; == &lt;valor int32&gt; antes de ir ao banco verifica se o valor é menor ou igual a 0 se for devolve um novo objeto.</para>
        /// <para>Se não retorna o objeto com o valor de id fronecido. Se nao existe, ou exite mais de 1, registro com o valor gera o erro InvalidOperationException (ver Enumerable.Single)</para>
        /// <para>Obs 2:</para>
        /// <para>Se usado com uma busca do tipo x=>x.&lt;colunapk int32&gt; == &lt;valor Nullable.int32&gt; antes de ir ao banco verifica se o valor é nulo se for devolve um novo objeto. </para>
        /// <para>Se não retorna o objeto com o valor de id fronecido. Se nao existe, ou exite mais de 1, registro com o valor gera o erro InvalidOperationException (ver Enumerable.Single)</para>
        /// <para>Obs 3:</para>
        /// <para>Se a busca é de outra natureza executa o SingleOrDefault(elemSearch). Se essa busca nao retorna elementos devolve um novo.</para>
        /// <para>Se a busca retorna mais de um elemento  gera o erro InvalidOperationException</para>
        /// <para>Caso contrario retorna o elemento devolvido pela busca.</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="elemSearch">uma função de busca que deve gerar um elemento ou nenhum.</param>
        /// <returns>Um elemento a ser alterado ou incluido. Ver obs: </returns>
        public static TEntity InsertOrUpdateOnSubmit<TEntity>(this DbSet<TEntity> source, Expression<Func<TEntity, bool>> elemSearch) where TEntity : class, new()
        {
            return source.InsertOrUpdateOnSubmit(elemSearch, new TEntity());
        }

        /// <summary>
        /// <para>Gera um registro pronto para ser alterado ou inclui o elemento novo fornecido no banco quando ocorrer o submit.</para>
        /// <para>Obs 1:</para>																																												
        /// <para>Se usado com uma busca do tipo x=>x.&lt;colunapk int32&gt; == &lt;valor int32&gt; antes de ir ao banco verifica se o valor é menor ou igual a 0 se for devolve um novo objeto.</para>
        /// <para>Se não retorna o objeto com o valor de id fronecido se este existir. Se exite mais de 1, gera-se o erro InvalidOperationException (ver Enumerable.SingleOrDefault)</para>
        /// <para>Obs 2:</para>
        /// <para>Se usado com uma busca do tipo x=>x.&lt;colunapk int32&gt; == &lt;valor Nullable.int32&gt; antes de ir ao banco verifica se o valor é nulo se for devolve um novo objeto. </para>
        /// <para>Se não retorna o objeto com o valor de id fronecido se este existir. Se exite mais de 1, gera-se o erro InvalidOperationException (ver Enumerable.SingleOrDefault)</para>
        /// <para>Obs 3:</para>
        /// <para>Se a busca é de outra natureza executa o SingleOrDefault(elemSearch). Se essa busca nao retorna elementos devolve um novo.</para>
        /// <para>Se a busca retorna mais de um elemento  gera o erro InvalidOperationException</para>
        /// <para>Caso contrario retorna o elemento devolvido pela busca.</para>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="elemSearch">uma função de busca que deve gerar um elemento ou nenhum.</param>
        /// <param name="novo">O novo elemento a ser incluido caso as buscas não retornem um registro</param>		
        /// <returns>Um elemento a ser alterado ou incluido. Ver obs: </returns>
        public static TEntity InsertOrUpdateOnSubmit<TEntity>(this DbSet<TEntity> source, Expression<Func<TEntity, bool>> elemSearch, TEntity novo) where TEntity : class
        {
            var bin = elemSearch.Body as BinaryExpression;
            if (bin == null)
                return source.SingleOrDefault(elemSearch) ?? source.InsertOnSubmitNew(novo);

            if (bin.Right.NodeType != ExpressionType.MemberAccess)
                return source.SingleOrDefault(elemSearch) ?? source.InsertOnSubmitNew(novo);

            if (bin.Right.Type == typeof(int))
            {
                var chave = Expression.Lambda<Func<int>>(bin.Right).Compile()();
                if (chave <= 0) return source.InsertOnSubmitNew(novo);
                //return source.Single(elemSearch);
            }

            if (bin.Right.Type == typeof(Nullable<int>))
            {
                var chave = Expression.Lambda<Func<int?>>(bin.Right).Compile()();
                if (!chave.HasValue) return source.InsertOnSubmitNew(novo);
                //return source.Single(elemSearch);
            }
            return source.SingleOrDefault(elemSearch) ?? source.InsertOnSubmitNew(novo);
        }

        /// <summary>
        /// Devolve um novo objeto deste tipo já vinculado ao InsertOnSubmit deste contexto.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TEntity InsertOnSubmitNew<TEntity>(this DbSet<TEntity> source) where TEntity : class, new()
        {
            return source.InsertOnSubmitNew(new TEntity());
        }

        /// <summary>
        /// Amarra o objeto passado ao InsertOnSubmit e devolve ele mesmo
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="novo"></param>
        /// <returns></returns>
        public static TEntity InsertOnSubmitNew<TEntity>(this DbSet<TEntity> source, TEntity novo) where TEntity : class
        {
            source.Add(novo);
            return novo;
        }

        public static T DecodeFromDB<T>(object valor)
        {
            if (valor == null)
                return default(T);

            if (valor.Equals(DBNull.Value))
                return default(T);

            var valnul = valor as INullable;
            if (valnul != null && valnul.IsNull)
                return default(T);

            var tipo = typeof(T);
            if (!tipo.IsValueType)
                if (tipo == typeof(string))
                    return (T)(valor.ToString().Trim() as object);
                else
                    throw new ArgumentException("O tipo de T não é um tipo esperado", "T");

            var tipobase = Nullable.GetUnderlyingType(tipo);
            if (tipobase == null)
                return (T)ConvertValueToType(tipo, valor);
            return (T)ConvertValueToType(tipobase, valor);
        }

        private static object ConvertValueToType(Type t, object valor)
        {
            var stringVal = valor.ToString();
            if (t == typeof(int))
            {
                if (valor is SqlInt16)
                    return ((SqlInt16)valor).Value;
                if (valor is SqlInt32)
                    return ((SqlInt32)valor).Value;

                int ret;
                int.TryParse(stringVal, out ret);
                return ret;
            }

            if (t == typeof(string))
            {
                if (valor is SqlString)
                    return ((SqlString)valor).Value;
                return stringVal;
            }

            if (t == typeof(DateTime))
            {
                if (valor is SqlDateTime)
                    return ((SqlDateTime)valor).Value;

                DateTime ret;
                DateTime.TryParse(stringVal, out ret);
                return ret;
            }

            if (t == typeof(bool))
            {
                if (valor is SqlBoolean)
                    return ((SqlBoolean)valor).Value;

                bool ret;
                bool.TryParse(stringVal, out ret);
                return ret;
            }

            if (t == typeof(decimal))
            {
                if (valor is SqlDecimal)
                    return ((SqlDecimal)valor).Value;

                decimal ret;
                decimal.TryParse(stringVal, out ret);
                return ret;
            }

            if (t == typeof(long))
            {
                if (valor is SqlInt64)
                    return ((SqlInt64)valor).Value;
                long ret;
                long.TryParse(stringVal, out ret);
                return ret;
            }

            if (t == typeof(float))
            {
                if (valor is SqlSingle)
                    return ((SqlSingle)valor).Value;
                float ret;
                float.TryParse(stringVal, out ret);
                return ret;
            }

            if (t == typeof(double))
            {
                if (valor is SqlDouble)
                    return ((SqlDouble)valor).Value;
                double ret;
                double.TryParse(stringVal, out ret);
                return ret;
            }

            if (t == typeof(short))
            {
                if (valor is SqlInt16)
                    return ((SqlInt16)valor).Value;
                short ret;
                short.TryParse(stringVal, out ret);
                return ret;
            }

            throw new ArgumentException("O tipo de T não é uma primitiva esperada", "T");
        }

        public static void ExecutaSQL(string nameStringConexao, string comandoSql, params SqlParameter[] paramList)
        {
            using (var sqlCon = new SqlConnection(GetDefaultConnString(nameStringConexao)))
            using (var cmd = new SqlCommand(comandoSql, sqlCon))
            {
                sqlCon.Open();
                try
                {
                    foreach (var parameter in paramList)
                    {
                        cmd.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    }
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    e.Data.Add("Comando", comandoSql);
                    throw;
                }
            }
        }

        public static string GetDefaultConnString(string nameStringConexao)
        {
            return ConfigurationManager.ConnectionStrings[nameStringConexao].ConnectionString;
        }
    }
}
