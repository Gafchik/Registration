using System.Runtime.Serialization;
using System.ServiceModel;

// ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Реструктуризация" можно использовать для одновременного изменения имени интерфейса "IService" в коде и файле конфигурации.
[ServiceContract]
public interface IService
{
	// метод для регестрации	
	[OperationContract]
	string Reg(User new_user);
	// метод для авторизации
	[OperationContract]
	string SingIn(string login, string pass);
	

	// TODO: Добавьте здесь операции служб
}

// Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
[DataContract]
public class User
{
	[DataMember]
	public int id { get; set; }
	[DataMember]
	public string Login { get; set; }
	[DataMember]
	public string Pass { get; set; }
	[DataMember]
	public string Name { get; set; }
	
}
