USE [verificador]
GO
/****** Object:  Table [dbo].[CanalesVenta]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CanalesVenta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[canalesVentaEvento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[canalesVentaEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idevento] [int] NULL,
	[idcanal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[categorias]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[categorias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](200) NULL,
	[fechaCreacion] [smalldatetime] NULL,
	[verCodigoArtesEscenicas] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[categoriasEvento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categoriasEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idEvento] [int] NULL,
	[idCategoria] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ciudades]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ciudades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[iddepartamento] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
	[codigo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[clientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipoPersona_id] [int] NOT NULL,
	[tipoDocumento_id] [int] NOT NULL,
	[identificacion] [varchar](100) NOT NULL,
	[primerNombre] [varchar](200) NOT NULL,
	[segundoNombre] [varchar](200) NULL,
	[primerApellido] [varchar](200) NOT NULL,
	[segundoApellido] [varchar](200) NULL,
	[fechaNacimiento] [smalldatetime] NOT NULL,
	[genero_id] [int] NOT NULL,
	[email] [varchar](200) NOT NULL,
	[celular] [varchar](100) NOT NULL,
	[telefonoCasa] [varchar](100) NULL,
	[telefonoOficina] [varchar](100) NULL,
	[jornadaEntrega] [varchar](100) NULL,
	[idCreatorby] [int] NULL,
	[dateCreated] [date] NULL,
	[idRetiredby] [int] NULL,
	[dateRetired] [date] NULL,
	[idChangedby] [int] NULL,
	[dateChanged] [date] NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK__clientes__3213E83F35A17576] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[consumo]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[consumo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [money] NULL,
	[descripcion] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[departamentos]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[departamentos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[idpais] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
	[codigo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[direccionesClientes]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[direccionesClientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ubicacion] [varchar](500) NOT NULL,
	[codigoPostal] [varchar](50) NULL,
	[ciudad_id] [int] NOT NULL,
	[cliente_id] [int] NOT NULL,
	[idCreatorby] [int] NULL,
	[dateCreated] [date] NULL,
	[idRetiredby] [int] NULL,
	[dateRetired] [date] NULL,
	[idChangedby] [int] NULL,
	[dateChanged] [date] NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK__direccio__3213E83F29A0899F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[empresarios]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[empresarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipoEmpresa_id] [int] NULL,
	[razonSocial] [varchar](200) NULL,
	[tipoDocumento_id] [int] NULL,
	[identificacion] [varchar](100) NULL,
	[tipoPersona_id] [int] NULL,
	[direccion] [varchar](300) NULL,
	[ciudad_id] [int] NULL,
	[telefono] [varchar](100) NULL,
	[celular] [varchar](100) NULL,
	[emailNotificacion] [varchar](200) NULL,
	[numeroRegistroArtesEscenicas] [varchar](100) NULL,
	[nombreRL] [varchar](200) NULL,
	[apellidosRL] [varchar](200) NULL,
	[tipoDocumentoRL_id] [int] NULL,
	[identificacionRL] [varchar](100) NULL,
	[sexo_idRL] [int] NULL,
	[emailRL] [varchar](200) NULL,
	[direccionRL] [varchar](300) NULL,
	[ciudad_idRL] [int] NULL,
	[telefonoRL] [varchar](100) NULL,
	[celularRL] [varchar](100) NULL,
	[ventasWebReacudoDirecto] [bit] NULL,
	[crateDate] [smalldatetime] NULL,
	[bloqueado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EstadoImpresion]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EstadoImpresion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[etapasVentaEvento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[etapasVentaEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[descripcion] [varchar](500) NULL,
	[tipo] [varchar](20) NULL,
	[fechaInicial] [smalldatetime] NULL,
	[fechaFinal] [smalldatetime] NULL,
	[idevento] [int] NULL,
	[LlevaCodigo] [bit] NULL,
	[Codigo] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[eventos]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[eventos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[multifuncion] [bit] NULL,
	[cantidadFunciones] [int] NULL,
	[fechaInicial] [smalldatetime] NULL,
	[fechaFinal] [smalldatetime] NULL,
	[empresario_id] [int] NULL,
	[tipoEvento] [int] NULL,
	[foto] [nvarchar](100) NULL,
	[descripcion] [nvarchar](max) NULL,
	[edadMinima] [int] NULL,
	[eventoPublico] [bit] NULL,
	[tipoRecaudo_id] [int] NULL,
	[entradasxPedidoOcompra] [int] NULL,
	[entradasxUsuario] [int] NULL,
	[aplicaServicio] [bit] NULL,
	[cantidadDiasValides] [int] NULL,
	[fechaCreacion] [smalldatetime] NULL,
	[AforoTotal] [int] NULL,
	[Estado] [varchar](20) NULL,
	[urlVideo] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[funcionesEventos]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[funcionesEventos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idevento] [int] NULL,
	[fechaIncial] [smalldatetime] NULL,
	[fechaFinal] [smalldatetime] NULL,
	[descripcion] [varchar](1000) NULL,
	[label] [varchar](50) NULL,
	[fechaCreacion] [smalldatetime] NULL,
	[aperturaPuertas] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Genero]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Genero](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Impuesto]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Impuesto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[valor] [decimal](18, 3) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[impuestosEvento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[impuestosEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idevento] [int] NULL,
	[idimpuesto] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[localidad]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[localidad](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Aforo] [int] NULL,
	[IdTipoLocalidad] [int] NULL,
	[FechaCreacion] [smalldatetime] NULL,
	[TicketNumeroInicio] [int] NULL,
	[TicketNumeroFinal] [int] NULL,
	[RangoTickets] [bit] NULL,
	[IdTipoNumeracion] [int] NULL,
	[Consumo] [varchar](100) NULL,
	[ValorConsumo] [money] NULL,
	[Filas] [int] NULL,
	[Sillas] [int] NULL,
	[OrientacionFilas] [varchar](20) NULL,
	[OrientacionSillas] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LocalidadesVenueEvento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalidadesVenueEvento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdVenue] [int] NULL,
	[IdLocalidad] [int] NULL,
	[IdEvento] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogPrintTicket]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LogPrintTicket](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[Estado] [varchar](50) NULL,
	[IdTransaccioTicketImpresion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mapas]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mapas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](100) NULL,
	[descripcion] [varchar](500) NULL,
	[capacidad] [int] NULL,
	[mapa] [nvarchar](1000) NULL,
	[fechaCreacion] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mapasVenue]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mapasVenue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idVenue] [int] NULL,
	[idMapa] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[paises]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[paises](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
	[codigo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[permisos]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[permisos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[descripcion] [varchar](200) NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[permisos_rol]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permisos_rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idpermiso] [int] NOT NULL,
	[idrol] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PrintersByComputers]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PrintersByComputers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MacAddress] [nvarchar](200) NULL,
	[PrinterName] [varchar](100) NULL,
	[TicketType] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[roles]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[roles_usuario]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles_usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idrol] [int] NOT NULL,
	[idusuario] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[serviciosEvento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[serviciosEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idevento] [int] NULL,
	[idservicio] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tickets]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tickets](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NULL,
	[barcode] [nvarchar](max) NULL,
	[numericBarcode] [varchar](50) NULL,
	[idEstado] [int] NULL,
	[fechaImpresion] [smalldatetime] NULL,
	[idTicketsEvento] [int] NULL,
	[idTicketsEtapa] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ticketsEtapa]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ticketsEtapa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idLocalidad] [int] NOT NULL,
	[idEtapa] [int] NOT NULL,
	[IdTipoServicio] [int] NULL,
	[Aforo] [int] NULL,
	[Precio] [money] NULL,
 CONSTRAINT [PK_ticketsEtapa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ticketsEvento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ticketsEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idEvento] [int] NULL,
	[cantidad] [int] NULL,
	[inicioNumeracion] [int] NULL,
	[JsonCanvas] [nvarchar](max) NULL,
	[finNumeracion] [int] NULL,
	[ticketType] [varchar](15) NULL,
	[ticketImage] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipodocumento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipodocumento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[codigo] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipoempresa]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipoempresa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[codigo] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoEvento]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoEvento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipoLocalidad]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipoLocalidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[descripcion] [varchar](200) NULL,
	[fechaCreacion] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipomoneda]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipomoneda](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[codigo] [varchar](10) NOT NULL,
	[simbolo] [varchar](10) NOT NULL,
	[decimales] [int] NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipoNumeracion]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipoNumeracion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Numeracion] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipopago]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipopago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipopersona]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipopersona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoRecaudo]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoRecaudo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoServicio]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoServicio](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](500) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tipoUsuario]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tipoUsuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](100) NULL,
	[description] [varchar](200) NULL,
	[createDate] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[transaccionPullTicketsImpresion]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transaccionPullTicketsImpresion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [smalldatetime] NULL,
	[Token] [nvarchar](200) NULL,
	[ImagenTicket] [nvarchar](max) NULL,
	[idTicket] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[transaccionTicketsImpresion]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transaccionTicketsImpresion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idTicket] [int] NULL,
	[token] [nvarchar](500) NULL,
	[fechaImpresion] [smalldatetime] NULL,
	[idEstado] [int] NULL,
	[idTicketEvento] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[contrasena] [varchar](80) NOT NULL,
	[bloqueado] [bit] NOT NULL,
	[email] [varchar](50) NOT NULL,
	[nombres] [varchar](50) NOT NULL,
	[apellidos] [varchar](50) NOT NULL,
	[direccion] [varchar](200) NULL,
	[telefono] [varchar](30) NULL,
	[fecha] [datetime] NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
	[tipoUsuario_id] [int] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[usuarios_portal]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios_portal](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contrasena] [varchar](80) NOT NULL,
	[bloqueado] [bit] NOT NULL,
	[email] [varchar](50) NOT NULL,
	[nombres] [varchar](80) NULL,
	[apellidos] [varchar](50) NULL,
	[fecha] [datetime] NULL,
	[fechacreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_usuarios_portal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UVT]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UVT](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[anio] [int] NOT NULL,
	[valor] [decimal](18, 3) NOT NULL,
	[fechacreacion] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venue]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[venue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](200) NULL,
	[direccion] [varchar](500) NULL,
	[ciudad_id] [int] NULL,
	[telefono] [varchar](100) NULL,
	[email] [varchar](200) NULL,
	[foto] [nvarchar](1000) NULL,
	[aforo] [int] NULL,
	[fechaCreacion] [smalldatetime] NULL,
	[Latitud] [varchar](100) NULL,
	[Longitud] [varchar](100) NULL,
	[CityName] [varchar](50) NULL,
	[DepartametName] [varchar](50) NULL,
	[CountryName] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[GeoCode] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CanalesVenta] ON 

GO
INSERT [dbo].[CanalesVenta] ([id], [nombre], [fechacreacion]) VALUES (1, N'test', CAST(N'2016-08-23 16:37:00.303' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[CanalesVenta] OFF
GO
SET IDENTITY_INSERT [dbo].[categorias] ON 

GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (2, N'Música', N'Música', CAST(N'2017-01-16 18:58:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (3, N'Artes Escénicas', N'Artes Escénicas', CAST(N'2017-01-16 18:59:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (4, N'Cursos y Talleres', N'Cursos y Talleres', CAST(N'2017-01-16 18:59:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (5, N'Ferias y Congresos', N'Ferias y Congresos', CAST(N'2017-01-16 19:00:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (6, N'Deportes', N'Deportes', CAST(N'2017-01-16 19:00:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (7, N'Cine', N'Cine', CAST(N'2017-01-16 19:00:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (8, N'Exhibiciones y Museos', N'Exhibiciones y Museos', CAST(N'2017-01-16 19:00:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (9, N'Turismo', N'Turismo', CAST(N'2017-01-16 19:01:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (10, N'Reuniones y Fiestas', N'Reuniones y Fiestas', CAST(N'2017-01-16 19:01:00' AS SmallDateTime), 0)
GO
INSERT [dbo].[categorias] ([id], [nombre], [descripcion], [fechaCreacion], [verCodigoArtesEscenicas]) VALUES (11, N'Otros', N'Otros', CAST(N'2017-01-16 19:01:00' AS SmallDateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[categoriasEvento] ON 

GO
INSERT [dbo].[categoriasEvento] ([id], [idEvento], [idCategoria]) VALUES (67, 68, 7)
GO
INSERT [dbo].[categoriasEvento] ([id], [idEvento], [idCategoria]) VALUES (68, 69, 7)
GO
INSERT [dbo].[categoriasEvento] ([id], [idEvento], [idCategoria]) VALUES (69, 70, 7)
GO
SET IDENTITY_INSERT [dbo].[categoriasEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[ciudades] ON 

GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1, N'LA CHORRERA', 1, CAST(N'2016-08-11 07:54:29.313' AS DateTime), N'07')
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (2, N'LA PEDRERA', 1, CAST(N'2016-08-11 07:54:29.313' AS DateTime), N'20')
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (3, N'LA VICTORIA', 1, CAST(N'2016-08-11 07:54:29.313' AS DateTime), N'10')
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (4, N'LETICIA', 1, CAST(N'2016-08-11 07:54:29.313' AS DateTime), N'22')
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (5, N'MIRITI', 1, CAST(N'2016-08-11 07:54:29.313' AS DateTime), N'10')
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (6, N'PUERTO ALEGRIA', 1, CAST(N'2016-08-11 07:54:29.317' AS DateTime), N'288')
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (7, N'PUERTO ARICA', 1, CAST(N'2016-08-11 07:54:29.317' AS DateTime), N'223')
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (8, N'PUERTO NARIÑO', 1, CAST(N'2016-08-11 07:54:29.317' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (9, N'PUERTO SANTANDER', 1, CAST(N'2016-08-11 07:54:29.317' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (10, N'TURAPACA', 1, CAST(N'2016-08-11 07:54:29.317' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (11, N'ABEJORRAL', 2, CAST(N'2016-08-11 07:54:29.317' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (12, N'ABRIAQUI', 2, CAST(N'2016-08-11 07:54:29.317' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (13, N'ALEJANDRIA', 2, CAST(N'2016-08-11 07:54:29.317' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (14, N'AMAGA', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (15, N'AMALFI', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (16, N'ANDES', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (17, N'ANGELOPOLIS', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (18, N'ANGOSTURA', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (19, N'ANORI', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (20, N'ANTIOQUIA', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (21, N'ANZA', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (22, N'APARTADO', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (23, N'ARBOLETES', 2, CAST(N'2016-08-11 07:54:29.320' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (24, N'ARGELIA', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (25, N'ARMENIA', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (26, N'BARBOSA', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (27, N'BELLO', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (28, N'BELMIRA', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (29, N'BETANIA', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (30, N'BETULIA', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (31, N'BOLIVAR', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (32, N'BRICEÑO', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (33, N'BURITICA', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (34, N'CACERES', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (35, N'CAICEDO', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (36, N'CALDAS', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (37, N'CAMPAMENTO', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (38, N'CANASGORDAS', 2, CAST(N'2016-08-11 07:54:29.323' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (39, N'CARACOLI', 2, CAST(N'2016-08-11 07:54:29.327' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (40, N'CARAMANTA', 2, CAST(N'2016-08-11 07:54:29.327' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (41, N'CAREPA', 2, CAST(N'2016-08-11 07:54:29.327' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (42, N'CARMEN DE VIBORAL', 2, CAST(N'2016-08-11 07:54:29.327' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (43, N'CAROLINA DEL PRINCIPE', 2, CAST(N'2016-08-11 07:54:29.327' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (44, N'CAUCASIA', 2, CAST(N'2016-08-11 07:54:29.327' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (45, N'CHIGORODO', 2, CAST(N'2016-08-11 07:54:29.327' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (46, N'CISNEROS', 2, CAST(N'2016-08-11 07:54:29.327' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (47, N'COCORNA', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (48, N'CONCEPCION', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (49, N'CONCORDIA', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (50, N'COPACABANA', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (51, N'DABEIBA', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (52, N'DONMATIAS', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (53, N'EBEJICO', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (54, N'EL BAGRE', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (55, N'EL PENOL', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (56, N'EL RETIRO', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (57, N'ENTRERRIOS', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (58, N'ENVIGADO', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (59, N'FREDONIA', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (60, N'FRONTINO', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (61, N'GIRALDO', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (62, N'GIRARDOTA', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (63, N'GOMEZ PLATA', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (64, N'GRANADA', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (65, N'GUADALUPE', 2, CAST(N'2016-08-11 07:54:29.330' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (66, N'GUARNE', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (67, N'GUATAQUE', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (68, N'HELICONIA', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (69, N'HISPANIA', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (70, N'ITAGUI', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (71, N'ITUANGO', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (72, N'JARDIN', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (73, N'JERICO', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (74, N'LA CEJA', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (75, N'LA ESTRELLA', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (76, N'LA PINTADA', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (77, N'LA UNION', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (78, N'LIBORINA', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (79, N'MACEO', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (80, N'MARINILLA', 2, CAST(N'2016-08-11 07:54:29.333' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (81, N'MEDELLIN', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (82, N'MONTEBELLO', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (83, N'MURINDO', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (84, N'MUTATA', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (85, N'NARINO', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (86, N'NECHI', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (87, N'NECOCLI', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (88, N'OLAYA', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (89, N'PEQUE', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (90, N'PUEBLORRICO', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (91, N'PUERTO BERRIO', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (92, N'PUERTO NARE', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (93, N'PUERTO TRIUNFO', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (94, N'REMEDIOS', 2, CAST(N'2016-08-11 07:54:29.337' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (95, N'RIONEGRO', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (96, N'SABANALARGA', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (97, N'SABANETA', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (98, N'SALGAR', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (99, N'SAN ANDRES DE CUERQUIA', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (100, N'SAN CARLOS', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (101, N'SAN FRANCISCO', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (102, N'SAN JERONIMO', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (103, N'SAN JOSE DE LA MONTAÑA', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (104, N'SAN JUAN DE URABA', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (105, N'SAN LUIS', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (106, N'SAN PEDRO DE LOS MILAGROS', 2, CAST(N'2016-08-11 07:54:29.340' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (107, N'SAN PEDRO DE URABA', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (108, N'SAN RAFAEL', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (109, N'SAN ROQUE', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (110, N'SAN VICENTE', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (111, N'SANTA BARBARA', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (112, N'SANTA ROSA DE OSOS', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (113, N'SANTO DOMINGO', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (114, N'SANTUARIO', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (115, N'SEGOVIA', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (116, N'SONSON', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (117, N'SOPETRAN', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (118, N'TAMESIS', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (119, N'TARAZA', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (120, N'TARSO', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (121, N'TITIRIBI', 2, CAST(N'2016-08-11 07:54:29.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (122, N'TOLEDO', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (123, N'TURBO', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (124, N'URAMITA', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (125, N'URRAO', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (126, N'VALDIVIA', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (127, N'VALPARAISO', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (128, N'VEGACHI', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (129, N'VENECIA', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (130, N'VIGIA DEL FUERTE', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (131, N'YALI', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (132, N'YARUMAL', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (133, N'YOLOMBO', 2, CAST(N'2016-08-11 07:54:29.347' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (134, N'YONDO', 2, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (135, N'ZARAGOZA', 2, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (136, N'ARAUCA', 3, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (137, N'ARAUQUITA', 3, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (138, N'CRAVO NORTE', 3, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (139, N'FORTUL', 3, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (140, N'PUERTO RONDON', 3, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (141, N'SARAVENA', 3, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (142, N'TAME', 3, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (143, N'BARANOA', 4, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (144, N'BARRANQUILLA', 4, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (145, N'CAMPO DE LA CRUZ', 4, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (146, N'CANDELARIA', 4, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (147, N'GALAPA', 4, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (148, N'JUAN DE ACOSTA', 4, CAST(N'2016-08-11 07:54:29.350' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (149, N'LURUACO', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (150, N'MALAMBO', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (151, N'MANATI', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (152, N'PALMAR DE VARELA', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (153, N'PIOJO', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (154, N'POLO NUEVO', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (155, N'PONEDERA', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (156, N'PUERTO COLOMBIA', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (157, N'REPELON', 4, CAST(N'2016-08-11 07:54:29.353' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (158, N'SABANAGRANDE', 4, CAST(N'2016-08-11 07:54:29.357' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (159, N'SABANALARGA', 4, CAST(N'2016-08-11 07:54:29.357' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (160, N'SANTA LUCIA', 4, CAST(N'2016-08-11 07:54:29.357' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (161, N'SANTO TOMAS', 4, CAST(N'2016-08-11 07:54:29.357' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (162, N'SOLEDAD', 4, CAST(N'2016-08-11 07:54:29.357' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (163, N'SUAN', 4, CAST(N'2016-08-11 07:54:29.357' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (164, N'TUBARA', 4, CAST(N'2016-08-11 07:54:29.360' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (165, N'USIACURI', 4, CAST(N'2016-08-11 07:54:29.360' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (166, N'ACHI', 5, CAST(N'2016-08-11 07:54:29.360' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (167, N'ALTOS DEL ROSARIO', 5, CAST(N'2016-08-11 07:54:29.360' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (168, N'ARENAL', 5, CAST(N'2016-08-11 07:54:29.360' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (169, N'ARJONA', 5, CAST(N'2016-08-11 07:54:29.360' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (170, N'ARROYOHONDO', 5, CAST(N'2016-08-11 07:54:29.360' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (171, N'BARRANCO DE LOBA', 5, CAST(N'2016-08-11 07:54:29.360' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (172, N'BRAZUELO DE PAPAYAL', 5, CAST(N'2016-08-11 07:54:29.363' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (173, N'CALAMAR', 5, CAST(N'2016-08-11 07:54:29.363' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (174, N'CANTAGALLO', 5, CAST(N'2016-08-11 07:54:29.363' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (175, N'CARTAGENA DE INDIAS', 5, CAST(N'2016-08-11 07:54:29.367' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (176, N'CICUCO', 5, CAST(N'2016-08-11 07:54:29.367' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (177, N'CLEMENCIA', 5, CAST(N'2016-08-11 07:54:29.367' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (178, N'CORDOBA', 5, CAST(N'2016-08-11 07:54:29.367' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (179, N'EL CARMEN DE BOLIVAR', 5, CAST(N'2016-08-11 07:54:29.367' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (180, N'EL GUAMO', 5, CAST(N'2016-08-11 07:54:29.370' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (181, N'EL PENION', 5, CAST(N'2016-08-11 07:54:29.370' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (182, N'HATILLO DE LOBA', 5, CAST(N'2016-08-11 07:54:29.370' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (183, N'MAGANGUE', 5, CAST(N'2016-08-11 07:54:29.370' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (184, N'MAHATES', 5, CAST(N'2016-08-11 07:54:29.370' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (185, N'MARGARITA', 5, CAST(N'2016-08-11 07:54:29.370' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (186, N'MARIA LA BAJA', 5, CAST(N'2016-08-11 07:54:29.370' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (187, N'MONTECRISTO', 5, CAST(N'2016-08-11 07:54:29.370' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (188, N'MORALES', 5, CAST(N'2016-08-11 07:54:29.373' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (189, N'MORALES', 5, CAST(N'2016-08-11 07:54:29.373' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (190, N'NOROSI', 5, CAST(N'2016-08-11 07:54:29.373' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (191, N'PINILLOS', 5, CAST(N'2016-08-11 07:54:29.373' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (192, N'REGIDOR', 5, CAST(N'2016-08-11 07:54:29.373' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (193, N'RIO VIEJO', 5, CAST(N'2016-08-11 07:54:29.377' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (194, N'SAN CRISTOBAL', 5, CAST(N'2016-08-11 07:54:29.377' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (195, N'SAN ESTANISLAO', 5, CAST(N'2016-08-11 07:54:29.377' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (196, N'SAN FERNANDO', 5, CAST(N'2016-08-11 07:54:29.377' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (197, N'SAN JACINTO', 5, CAST(N'2016-08-11 07:54:29.377' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (198, N'SAN JACINTO DEL CAUCA', 5, CAST(N'2016-08-11 07:54:29.377' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (199, N'SAN JUAN DE NEPOMUCENO', 5, CAST(N'2016-08-11 07:54:29.377' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (200, N'SAN MARTIN DE LOBA', 5, CAST(N'2016-08-11 07:54:29.380' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (201, N'SAN PABLO', 5, CAST(N'2016-08-11 07:54:29.380' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (202, N'SAN PABLO NORTE', 5, CAST(N'2016-08-11 07:54:29.380' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (203, N'SANTA CATALINA', 5, CAST(N'2016-08-11 07:54:29.380' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (204, N'SANTA CRUZ DE MOMPOX', 5, CAST(N'2016-08-11 07:54:29.380' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (205, N'SANTA ROSA', 5, CAST(N'2016-08-11 07:54:29.380' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (206, N'SANTA ROSA DEL SUR', 5, CAST(N'2016-08-11 07:54:29.380' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (207, N'SIMITI', 5, CAST(N'2016-08-11 07:54:29.380' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (208, N'SOPLAVIENTO', 5, CAST(N'2016-08-11 07:54:29.383' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (209, N'TALAIGUA NUEVO', 5, CAST(N'2016-08-11 07:54:29.383' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (210, N'TUQUISIO', 5, CAST(N'2016-08-11 07:54:29.383' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (211, N'TURBACO', 5, CAST(N'2016-08-11 07:54:29.383' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (212, N'TURBANA', 5, CAST(N'2016-08-11 07:54:29.383' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (213, N'VILLANUEVA', 5, CAST(N'2016-08-11 07:54:29.383' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (214, N'ZAMBRANO', 5, CAST(N'2016-08-11 07:54:29.387' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (215, N'AQUITANIA', 6, CAST(N'2016-08-11 07:54:29.387' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (216, N'ARCABUCO', 6, CAST(N'2016-08-11 07:54:29.387' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (217, N'BELÉN', 6, CAST(N'2016-08-11 07:54:29.387' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (218, N'BERBEO', 6, CAST(N'2016-08-11 07:54:29.387' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (219, N'BETÉITIVA', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (220, N'BOAVITA', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (221, N'BOYACÁ', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (222, N'BRICEÑO', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (223, N'BUENAVISTA', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (224, N'BUSBANZÁ', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (225, N'CALDAS', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (226, N'CAMPO HERMOSO', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (227, N'CERINZA', 6, CAST(N'2016-08-11 07:54:29.390' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (228, N'CHINAVITA', 6, CAST(N'2016-08-11 07:54:29.393' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (229, N'CHIQUINQUIRÁ', 6, CAST(N'2016-08-11 07:54:29.393' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (230, N'CHÍQUIZA', 6, CAST(N'2016-08-11 07:54:29.393' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (231, N'CHISCAS', 6, CAST(N'2016-08-11 07:54:29.393' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (232, N'CHITA', 6, CAST(N'2016-08-11 07:54:29.393' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (233, N'CHITARAQUE', 6, CAST(N'2016-08-11 07:54:29.397' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (234, N'CHIVATÁ', 6, CAST(N'2016-08-11 07:54:29.397' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (235, N'CIÉNEGA', 6, CAST(N'2016-08-11 07:54:29.397' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (236, N'CÓMBITA', 6, CAST(N'2016-08-11 07:54:29.397' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (237, N'COPER', 6, CAST(N'2016-08-11 07:54:29.400' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (238, N'CORRALES', 6, CAST(N'2016-08-11 07:54:29.400' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (239, N'COVARACHÍA', 6, CAST(N'2016-08-11 07:54:29.400' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (240, N'CUBARA', 6, CAST(N'2016-08-11 07:54:29.400' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (241, N'CUCAITA', 6, CAST(N'2016-08-11 07:54:29.400' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (242, N'CUITIVA', 6, CAST(N'2016-08-11 07:54:29.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (243, N'DUITAMA', 6, CAST(N'2016-08-11 07:54:29.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (244, N'EL COCUY', 6, CAST(N'2016-08-11 07:54:29.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (245, N'EL ESPINO', 6, CAST(N'2016-08-11 07:54:29.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (246, N'FIRAVITOBA', 6, CAST(N'2016-08-11 07:54:29.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (247, N'FLORESTA', 6, CAST(N'2016-08-11 07:54:29.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (248, N'GACHANTIVÁ', 6, CAST(N'2016-08-11 07:54:29.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (249, N'GÁMEZA', 6, CAST(N'2016-08-11 07:54:29.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (250, N'GARAGOA', 6, CAST(N'2016-08-11 07:54:29.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (251, N'GUACAMAYAS', 6, CAST(N'2016-08-11 07:54:29.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (252, N'GÜICÁN', 6, CAST(N'2016-08-11 07:54:29.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (253, N'IZA', 6, CAST(N'2016-08-11 07:54:29.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (254, N'JENESANO', 6, CAST(N'2016-08-11 07:54:29.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (255, N'JERICÓ', 6, CAST(N'2016-08-11 07:54:29.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (256, N'LA UVITA', 6, CAST(N'2016-08-11 07:54:29.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (257, N'LA VICTORIA', 6, CAST(N'2016-08-11 07:54:29.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (258, N'LABRANZA GRANDE', 6, CAST(N'2016-08-11 07:54:29.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (259, N'MACANAL', 6, CAST(N'2016-08-11 07:54:29.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (260, N'MARIPÍ', 6, CAST(N'2016-08-11 07:54:29.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (261, N'MIRAFLORES', 6, CAST(N'2016-08-11 07:54:29.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (262, N'MONGUA', 6, CAST(N'2016-08-11 07:54:29.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (263, N'MONGUÍ', 6, CAST(N'2016-08-11 07:54:29.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (264, N'MONIQUIRÁ', 6, CAST(N'2016-08-11 07:54:29.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (265, N'MOTAVITA', 6, CAST(N'2016-08-11 07:54:29.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (266, N'MUZO', 6, CAST(N'2016-08-11 07:54:29.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (267, N'NOBSA', 6, CAST(N'2016-08-11 07:54:29.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (268, N'NUEVO COLÓN', 6, CAST(N'2016-08-11 07:54:29.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (269, N'OICATÁ', 6, CAST(N'2016-08-11 07:54:29.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (270, N'OTANCHE', 6, CAST(N'2016-08-11 07:54:29.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (271, N'PACHAVITA', 6, CAST(N'2016-08-11 07:54:29.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (272, N'PÁEZ', 6, CAST(N'2016-08-11 07:54:29.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (273, N'PAIPA', 6, CAST(N'2016-08-11 07:54:29.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (274, N'PAJARITO', 6, CAST(N'2016-08-11 07:54:29.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (275, N'PANQUEBA', 6, CAST(N'2016-08-11 07:54:29.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (276, N'PAUNA', 6, CAST(N'2016-08-11 07:54:29.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (277, N'PAYA', 6, CAST(N'2016-08-11 07:54:29.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (278, N'PAZ DE RÍO', 6, CAST(N'2016-08-11 07:54:29.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (279, N'PESCA', 6, CAST(N'2016-08-11 07:54:29.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (280, N'PISBA', 6, CAST(N'2016-08-11 07:54:29.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (281, N'PUERTO BOYACA', 6, CAST(N'2016-08-11 07:54:29.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (282, N'QUÍPAMA', 6, CAST(N'2016-08-11 07:54:29.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (283, N'RAMIRIQUÍ', 6, CAST(N'2016-08-11 07:54:29.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (284, N'RÁQUIRA', 6, CAST(N'2016-08-11 07:54:29.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (285, N'RONDÓN', 6, CAST(N'2016-08-11 07:54:29.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (286, N'SABOYÁ', 6, CAST(N'2016-08-11 07:54:29.443' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (287, N'SÁCHICA', 6, CAST(N'2016-08-11 07:54:29.443' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (288, N'SAMACÁ', 6, CAST(N'2016-08-11 07:54:29.443' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (289, N'SAN EDUARDO', 6, CAST(N'2016-08-11 07:54:29.443' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (290, N'SAN JOSÉ DE PARE', 6, CAST(N'2016-08-11 07:54:29.443' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (291, N'SAN LUÍS DE GACENO', 6, CAST(N'2016-08-11 07:54:29.447' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (292, N'SAN MATEO', 6, CAST(N'2016-08-11 07:54:29.447' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (293, N'SAN MIGUEL DE SEMA', 6, CAST(N'2016-08-11 07:54:29.447' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (294, N'SAN PABLO DE BORBUR', 6, CAST(N'2016-08-11 07:54:29.447' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (295, N'SANTA MARÍA', 6, CAST(N'2016-08-11 07:54:29.450' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (296, N'SANTA ROSA DE VITERBO', 6, CAST(N'2016-08-11 07:54:29.450' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (297, N'SANTA SOFÍA', 6, CAST(N'2016-08-11 07:54:29.450' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (298, N'SANTANA', 6, CAST(N'2016-08-11 07:54:29.450' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (299, N'SATIVANORTE', 6, CAST(N'2016-08-11 07:54:29.450' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (300, N'SATIVASUR', 6, CAST(N'2016-08-11 07:54:29.450' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (301, N'SIACHOQUE', 6, CAST(N'2016-08-11 07:54:29.450' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (302, N'SOATÁ', 6, CAST(N'2016-08-11 07:54:29.450' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (303, N'SOCHA', 6, CAST(N'2016-08-11 07:54:29.453' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (304, N'SOCOTÁ', 6, CAST(N'2016-08-11 07:54:29.453' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (305, N'SOGAMOSO', 6, CAST(N'2016-08-11 07:54:29.453' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (306, N'SORA', 6, CAST(N'2016-08-11 07:54:29.453' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (307, N'SORACÁ', 6, CAST(N'2016-08-11 07:54:29.493' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (308, N'SOTAQUIRÁ', 6, CAST(N'2016-08-11 07:54:29.493' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (309, N'SUSACÓN', 6, CAST(N'2016-08-11 07:54:29.493' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (310, N'SUTARMACHÁN', 6, CAST(N'2016-08-11 07:54:29.493' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (311, N'TASCO', 6, CAST(N'2016-08-11 07:54:29.493' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (312, N'TIBANÁ', 6, CAST(N'2016-08-11 07:54:29.493' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (313, N'TIBASOSA', 6, CAST(N'2016-08-11 07:54:29.497' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (314, N'TINJACÁ', 6, CAST(N'2016-08-11 07:54:29.497' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (315, N'TIPACOQUE', 6, CAST(N'2016-08-11 07:54:29.497' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (316, N'TOCA', 6, CAST(N'2016-08-11 07:54:29.497' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (317, N'TOGÜÍ', 6, CAST(N'2016-08-11 07:54:29.497' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (318, N'TÓPAGA', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (319, N'TOTA', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (320, N'TUNJA', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (321, N'TUNUNGUÁ', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (322, N'TURMEQUÉ', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (323, N'TUTA', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (324, N'TUTAZÁ', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (325, N'UMBITA', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (326, N'VENTA QUEMADA', 6, CAST(N'2016-08-11 07:54:29.500' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (327, N'VILLA DE LEYVA', 6, CAST(N'2016-08-11 07:54:29.503' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (328, N'VIRACACHÁ', 6, CAST(N'2016-08-11 07:54:29.503' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (329, N'ZETAQUIRA', 6, CAST(N'2016-08-11 07:54:29.503' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (330, N'AGUADAS', 7, CAST(N'2016-08-11 07:54:29.503' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (331, N'ANSERMA', 7, CAST(N'2016-08-11 07:54:29.503' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (332, N'ARANZAZU', 7, CAST(N'2016-08-11 07:54:29.503' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (333, N'BELALCAZAR', 7, CAST(N'2016-08-11 07:54:29.507' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (334, N'CHINCHINÁ', 7, CAST(N'2016-08-11 07:54:29.507' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (335, N'FILADELFIA', 7, CAST(N'2016-08-11 07:54:29.507' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (336, N'LA DORADA', 7, CAST(N'2016-08-11 07:54:29.507' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (337, N'LA MERCED', 7, CAST(N'2016-08-11 07:54:29.507' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (338, N'MANIZALES', 7, CAST(N'2016-08-11 07:54:29.507' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (339, N'MANZANARES', 7, CAST(N'2016-08-11 07:54:29.507' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (340, N'MARMATO', 7, CAST(N'2016-08-11 07:54:29.510' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (341, N'MARQUETALIA', 7, CAST(N'2016-08-11 07:54:29.510' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (342, N'MARULANDA', 7, CAST(N'2016-08-11 07:54:29.510' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (343, N'NEIRA', 7, CAST(N'2016-08-11 07:54:29.510' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (344, N'NORCASIA', 7, CAST(N'2016-08-11 07:54:29.513' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (345, N'PACORA', 7, CAST(N'2016-08-11 07:54:29.513' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (346, N'PALESTINA', 7, CAST(N'2016-08-11 07:54:29.513' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (347, N'PENSILVANIA', 7, CAST(N'2016-08-11 07:54:29.513' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (348, N'RIOSUCIO', 7, CAST(N'2016-08-11 07:54:29.513' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (349, N'RISARALDA', 7, CAST(N'2016-08-11 07:54:29.513' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (350, N'SALAMINA', 7, CAST(N'2016-08-11 07:54:29.517' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (351, N'SAMANA', 7, CAST(N'2016-08-11 07:54:29.517' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (352, N'SAN JOSE', 7, CAST(N'2016-08-11 07:54:29.517' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (353, N'SUPÍA', 7, CAST(N'2016-08-11 07:54:29.517' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (354, N'VICTORIA', 7, CAST(N'2016-08-11 07:54:29.520' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (355, N'VILLAMARÍA', 7, CAST(N'2016-08-11 07:54:29.520' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (356, N'VITERBO', 7, CAST(N'2016-08-11 07:54:29.520' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (357, N'ALBANIA', 8, CAST(N'2016-08-11 07:54:29.520' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (358, N'BELÉN ANDAQUIES', 8, CAST(N'2016-08-11 07:54:29.520' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (359, N'CARTAGENA DEL CHAIRA', 8, CAST(N'2016-08-11 07:54:29.520' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (360, N'CURILLO', 8, CAST(N'2016-08-11 07:54:29.520' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (361, N'EL DONCELLO', 8, CAST(N'2016-08-11 07:54:29.520' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (362, N'EL PAUJIL', 8, CAST(N'2016-08-11 07:54:29.523' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (363, N'FLORENCIA', 8, CAST(N'2016-08-11 07:54:29.523' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (364, N'LA MONTAÑITA', 8, CAST(N'2016-08-11 07:54:29.527' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (365, N'MILÁN', 8, CAST(N'2016-08-11 07:54:29.527' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (366, N'MORELIA', 8, CAST(N'2016-08-11 07:54:29.527' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (367, N'PUERTO RICO', 8, CAST(N'2016-08-11 07:54:29.527' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (368, N'SAN  VICENTE DEL CAGUAN', 8, CAST(N'2016-08-11 07:54:29.527' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (369, N'SAN JOSÉ DE FRAGUA', 8, CAST(N'2016-08-11 07:54:29.527' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (370, N'SOLANO', 8, CAST(N'2016-08-11 07:54:29.527' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (371, N'SOLITA', 8, CAST(N'2016-08-11 07:54:29.530' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (372, N'VALPARAÍSO', 8, CAST(N'2016-08-11 07:54:29.530' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (373, N'AGUAZUL', 9, CAST(N'2016-08-11 07:54:29.530' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (374, N'CHAMEZA', 9, CAST(N'2016-08-11 07:54:29.530' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (375, N'HATO COROZAL', 9, CAST(N'2016-08-11 07:54:29.537' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (376, N'LA SALINA', 9, CAST(N'2016-08-11 07:54:29.537' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (377, N'MANÍ', 9, CAST(N'2016-08-11 07:54:29.597' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (378, N'MONTERREY', 9, CAST(N'2016-08-11 07:54:29.600' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (379, N'NUNCHIA', 9, CAST(N'2016-08-11 07:54:29.600' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (380, N'OROCUE', 9, CAST(N'2016-08-11 07:54:29.600' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (381, N'PAZ DE ARIPORO', 9, CAST(N'2016-08-11 07:54:29.600' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (382, N'PORE', 9, CAST(N'2016-08-11 07:54:29.600' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (383, N'RECETOR', 9, CAST(N'2016-08-11 07:54:29.600' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (384, N'SABANA LARGA', 9, CAST(N'2016-08-11 07:54:29.600' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (385, N'SACAMA', 9, CAST(N'2016-08-11 07:54:29.600' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (386, N'SAN LUIS DE PALENQUE', 9, CAST(N'2016-08-11 07:54:29.603' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (387, N'TAMARA', 9, CAST(N'2016-08-11 07:54:29.603' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (388, N'TAURAMENA', 9, CAST(N'2016-08-11 07:54:29.603' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (389, N'TRINIDAD', 9, CAST(N'2016-08-11 07:54:29.603' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (390, N'VILLANUEVA', 9, CAST(N'2016-08-11 07:54:29.603' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (391, N'YOPAL', 9, CAST(N'2016-08-11 07:54:29.607' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (392, N'ALMAGUER', 10, CAST(N'2016-08-11 07:54:29.607' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (393, N'ARGELIA', 10, CAST(N'2016-08-11 07:54:29.607' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (394, N'BALBOA', 10, CAST(N'2016-08-11 07:54:29.610' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (395, N'BOLÍVAR', 10, CAST(N'2016-08-11 07:54:29.610' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (396, N'BUENOS AIRES', 10, CAST(N'2016-08-11 07:54:29.610' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (397, N'CAJIBIO', 10, CAST(N'2016-08-11 07:54:29.610' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (398, N'CALDONO', 10, CAST(N'2016-08-11 07:54:29.610' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (399, N'CALOTO', 10, CAST(N'2016-08-11 07:54:29.610' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (400, N'CORINTO', 10, CAST(N'2016-08-11 07:54:29.610' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (401, N'EL TAMBO', 10, CAST(N'2016-08-11 07:54:29.610' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (402, N'FLORENCIA', 10, CAST(N'2016-08-11 07:54:29.613' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (403, N'GUAPI', 10, CAST(N'2016-08-11 07:54:29.613' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (404, N'INZA', 10, CAST(N'2016-08-11 07:54:29.613' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (405, N'JAMBALÓ', 10, CAST(N'2016-08-11 07:54:29.613' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (406, N'LA SIERRA', 10, CAST(N'2016-08-11 07:54:29.613' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (407, N'LA VEGA', 10, CAST(N'2016-08-11 07:54:29.613' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (408, N'LÓPEZ', 10, CAST(N'2016-08-11 07:54:29.617' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (409, N'MERCADERES', 10, CAST(N'2016-08-11 07:54:29.617' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (410, N'MIRANDA', 10, CAST(N'2016-08-11 07:54:29.620' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (411, N'MORALES', 10, CAST(N'2016-08-11 07:54:29.620' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (412, N'PADILLA', 10, CAST(N'2016-08-11 07:54:29.620' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (413, N'PÁEZ', 10, CAST(N'2016-08-11 07:54:29.620' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (414, N'PATIA (EL BORDO)', 10, CAST(N'2016-08-11 07:54:29.620' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (415, N'PIAMONTE', 10, CAST(N'2016-08-11 07:54:29.620' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (416, N'PIENDAMO', 10, CAST(N'2016-08-11 07:54:29.620' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (417, N'POPAYÁN', 10, CAST(N'2016-08-11 07:54:29.620' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (418, N'PUERTO TEJADA', 10, CAST(N'2016-08-11 07:54:29.623' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (419, N'PURACE', 10, CAST(N'2016-08-11 07:54:29.623' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (420, N'ROSAS', 10, CAST(N'2016-08-11 07:54:29.623' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (421, N'SAN SEBASTIÁN', 10, CAST(N'2016-08-11 07:54:29.623' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (422, N'SANTA ROSA', 10, CAST(N'2016-08-11 07:54:29.623' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (423, N'SANTANDER DE QUILICHAO', 10, CAST(N'2016-08-11 07:54:29.627' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (424, N'SILVIA', 10, CAST(N'2016-08-11 07:54:29.627' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (425, N'SOTARA', 10, CAST(N'2016-08-11 07:54:29.627' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (426, N'SUÁREZ', 10, CAST(N'2016-08-11 07:54:29.627' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (427, N'SUCRE', 10, CAST(N'2016-08-11 07:54:29.627' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (428, N'TIMBÍO', 10, CAST(N'2016-08-11 07:54:29.627' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (429, N'TIMBIQUÍ', 10, CAST(N'2016-08-11 07:54:29.630' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (430, N'TORIBIO', 10, CAST(N'2016-08-11 07:54:29.630' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (431, N'TOTORO', 10, CAST(N'2016-08-11 07:54:29.630' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (432, N'VILLA RICA', 10, CAST(N'2016-08-11 07:54:29.630' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (433, N'AGUACHICA', 11, CAST(N'2016-08-11 07:54:29.630' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (434, N'AGUSTÍN CODAZZI', 11, CAST(N'2016-08-11 07:54:29.630' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (435, N'ASTREA', 11, CAST(N'2016-08-11 07:54:29.630' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (436, N'BECERRIL', 11, CAST(N'2016-08-11 07:54:29.633' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (437, N'BOSCONIA', 11, CAST(N'2016-08-11 07:54:29.633' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (438, N'CHIMICHAGUA', 11, CAST(N'2016-08-11 07:54:29.633' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (439, N'CHIRIGUANÁ', 11, CAST(N'2016-08-11 07:54:29.637' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (440, N'CURUMANÍ', 11, CAST(N'2016-08-11 07:54:29.637' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (441, N'EL COPEY', 11, CAST(N'2016-08-11 07:54:29.637' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (442, N'EL PASO', 11, CAST(N'2016-08-11 07:54:29.637' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (443, N'GAMARRA', 11, CAST(N'2016-08-11 07:54:29.637' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (444, N'GONZÁLEZ', 11, CAST(N'2016-08-11 07:54:29.637' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (445, N'LA GLORIA', 11, CAST(N'2016-08-11 07:54:29.640' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (446, N'LA JAGUA IBIRICO', 11, CAST(N'2016-08-11 07:54:29.640' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (447, N'MANAURE BALCÓN DEL CESAR', 11, CAST(N'2016-08-11 07:54:29.640' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (448, N'PAILITAS', 11, CAST(N'2016-08-11 07:54:29.640' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (449, N'PELAYA', 11, CAST(N'2016-08-11 07:54:29.640' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (450, N'PUEBLO BELLO', 11, CAST(N'2016-08-11 07:54:29.640' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (451, N'RÍO DE ORO', 11, CAST(N'2016-08-11 07:54:29.640' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (452, N'ROBLES (LA PAZ)', 11, CAST(N'2016-08-11 07:54:29.640' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (453, N'SAN ALBERTO', 11, CAST(N'2016-08-11 07:54:29.643' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (454, N'SAN DIEGO', 11, CAST(N'2016-08-11 07:54:29.643' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (455, N'SAN MARTÍN', 11, CAST(N'2016-08-11 07:54:29.643' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (456, N'TAMALAMEQUE', 11, CAST(N'2016-08-11 07:54:29.643' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (457, N'VALLEDUPAR', 11, CAST(N'2016-08-11 07:54:29.643' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (458, N'ACANDI', 12, CAST(N'2016-08-11 07:54:29.643' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (459, N'ALTO BAUDO (PIE DE PATO)', 12, CAST(N'2016-08-11 07:54:29.647' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (460, N'ATRATO', 12, CAST(N'2016-08-11 07:54:29.647' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (461, N'BAGADO', 12, CAST(N'2016-08-11 07:54:29.647' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (462, N'BAHIA SOLANO (MUTIS)', 12, CAST(N'2016-08-11 07:54:29.647' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (463, N'BAJO BAUDO (PIZARRO)', 12, CAST(N'2016-08-11 07:54:29.647' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (464, N'BOJAYA (BELLAVISTA)', 12, CAST(N'2016-08-11 07:54:29.650' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (465, N'CANTON DE SAN PABLO', 12, CAST(N'2016-08-11 07:54:29.650' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (466, N'CARMEN DEL DARIEN', 12, CAST(N'2016-08-11 07:54:29.650' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (467, N'CERTEGUI', 12, CAST(N'2016-08-11 07:54:29.650' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (468, N'CONDOTO', 12, CAST(N'2016-08-11 07:54:29.650' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (469, N'EL CARMEN', 12, CAST(N'2016-08-11 07:54:29.650' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (470, N'ISTMINA', 12, CAST(N'2016-08-11 07:54:29.650' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (471, N'JURADO', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (472, N'LITORAL DEL SAN JUAN', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (473, N'LLORO', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (474, N'MEDIO ATRATO', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (475, N'MEDIO BAUDO (BOCA DE PEPE)', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (476, N'MEDIO SAN JUAN', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (477, N'NOVITA', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (478, N'NUQUI', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (479, N'QUIBDO', 12, CAST(N'2016-08-11 07:54:29.653' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (480, N'RIO IRO', 12, CAST(N'2016-08-11 07:54:29.657' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (481, N'RIO QUITO', 12, CAST(N'2016-08-11 07:54:29.657' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (482, N'RIOSUCIO', 12, CAST(N'2016-08-11 07:54:29.657' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (483, N'SAN JOSE DEL PALMAR', 12, CAST(N'2016-08-11 07:54:29.657' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (484, N'SIPI', 12, CAST(N'2016-08-11 07:54:29.657' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (485, N'TADO', 12, CAST(N'2016-08-11 07:54:29.657' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (486, N'UNGUIA', 12, CAST(N'2016-08-11 07:54:29.657' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (487, N'UNIÓN PANAMERICANA', 12, CAST(N'2016-08-11 07:54:29.657' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (488, N'AYAPEL', 13, CAST(N'2016-08-11 07:54:29.660' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (489, N'BUENAVISTA', 13, CAST(N'2016-08-11 07:54:29.673' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (490, N'CANALETE', 13, CAST(N'2016-08-11 07:54:29.673' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (491, N'CERETÉ', 13, CAST(N'2016-08-11 07:54:29.673' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (492, N'CHIMA', 13, CAST(N'2016-08-11 07:54:29.673' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (493, N'CHINÚ', 13, CAST(N'2016-08-11 07:54:29.673' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (494, N'CIENAGA DE ORO', 13, CAST(N'2016-08-11 07:54:29.673' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (495, N'COTORRA', 13, CAST(N'2016-08-11 07:54:29.673' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (496, N'LA APARTADA', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (497, N'LORICA', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (498, N'LOS CÓRDOBAS', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (499, N'MOMIL', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (500, N'MONTELÍBANO', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (501, N'MONTERÍA', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (502, N'MOÑITOS', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (503, N'PLANETA RICA', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (504, N'PUEBLO NUEVO', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (505, N'PUERTO ESCONDIDO', 13, CAST(N'2016-08-11 07:54:29.677' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (506, N'PUERTO LIBERTADOR', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (507, N'PURÍSIMA', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (508, N'SAHAGÚN', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (509, N'SAN ANDRÉS SOTAVENTO', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (510, N'SAN ANTERO', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (511, N'SAN BERNARDO VIENTO', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (512, N'SAN CARLOS', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (513, N'SAN PELAYO', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (514, N'TIERRALTA', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (515, N'VALENCIA', 13, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (516, N'AGUA DE DIOS', 14, CAST(N'2016-08-11 07:54:29.680' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (517, N'ALBAN', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (518, N'ANAPOIMA', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (519, N'ANOLAIMA', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (520, N'ARBELAEZ', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (521, N'BELTRÁN', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (522, N'BITUIMA', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (523, N'BOGOTÁ DC', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (524, N'BOJACÁ', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (525, N'CABRERA', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (526, N'CACHIPAY', 14, CAST(N'2016-08-11 07:54:29.683' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (527, N'CAJICÁ', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (528, N'CAPARRAPÍ', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (529, N'CAQUEZA', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (530, N'CARMEN DE CARUPA', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (531, N'CHAGUANÍ', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (532, N'CHIA', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (533, N'CHIPAQUE', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (534, N'CHOACHÍ', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (535, N'CHOCONTÁ', 14, CAST(N'2016-08-11 07:54:29.687' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (536, N'COGUA', 14, CAST(N'2016-08-11 07:54:29.690' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (537, N'COTA', 14, CAST(N'2016-08-11 07:54:29.690' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (538, N'CUCUNUBÁ', 14, CAST(N'2016-08-11 07:54:29.690' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (539, N'EL COLEGIO', 14, CAST(N'2016-08-11 07:54:29.690' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (540, N'EL PEÑÓN', 14, CAST(N'2016-08-11 07:54:29.690' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (541, N'EL ROSAL1', 14, CAST(N'2016-08-11 07:54:29.690' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (542, N'FACATATIVA', 14, CAST(N'2016-08-11 07:54:29.690' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (543, N'FÓMEQUE', 14, CAST(N'2016-08-11 07:54:29.690' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (544, N'FOSCA', 14, CAST(N'2016-08-11 07:54:29.693' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (545, N'FUNZA', 14, CAST(N'2016-08-11 07:54:29.693' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (546, N'FÚQUENE', 14, CAST(N'2016-08-11 07:54:29.693' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (547, N'FUSAGASUGA', 14, CAST(N'2016-08-11 07:54:29.693' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (548, N'GACHALÁ', 14, CAST(N'2016-08-11 07:54:29.693' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (549, N'GACHANCIPÁ', 14, CAST(N'2016-08-11 07:54:29.693' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (550, N'GACHETA', 14, CAST(N'2016-08-11 07:54:29.693' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (551, N'GAMA', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (552, N'GIRARDOT', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (553, N'GRANADA2', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (554, N'GUACHETÁ', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (555, N'GUADUAS', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (556, N'GUASCA', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (557, N'GUATAQUÍ', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (558, N'GUATAVITA', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (559, N'GUAYABAL DE SIQUIMA', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (560, N'GUAYABETAL', 14, CAST(N'2016-08-11 07:54:29.697' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (561, N'GUTIÉRREZ', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (562, N'JERUSALÉN', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (563, N'JUNÍN', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (564, N'LA CALERA', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (565, N'LA MESA', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (566, N'LA PALMA', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (567, N'LA PEÑA', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (568, N'LA VEGA', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (569, N'LENGUAZAQUE', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (570, N'MACHETÁ', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (571, N'MADRID', 14, CAST(N'2016-08-11 07:54:29.700' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (572, N'MANTA', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (573, N'MEDINA', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (574, N'MOSQUERA', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (575, N'NARIÑO', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (576, N'NEMOCÓN', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (577, N'NILO', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (578, N'NIMAIMA', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (579, N'NOCAIMA', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (580, N'OSPINA PÉREZ', 14, CAST(N'2016-08-11 07:54:29.703' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (581, N'PACHO', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (582, N'PAIME', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (583, N'PANDI', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (584, N'PARATEBUENO', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (585, N'PASCA', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (586, N'PUERTO SALGAR', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (587, N'PULÍ', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (588, N'QUEBRADANEGRA', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (589, N'QUETAME', 14, CAST(N'2016-08-11 07:54:29.707' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (590, N'QUIPILE', 14, CAST(N'2016-08-11 07:54:29.710' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (591, N'RAFAEL REYES', 14, CAST(N'2016-08-11 07:54:29.710' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (592, N'RICAURTE', 14, CAST(N'2016-08-11 07:54:29.710' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (593, N'SAN  ANTONIO DEL  TEQUENDAMA', 14, CAST(N'2016-08-11 07:54:29.710' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (594, N'SAN BERNARDO', 14, CAST(N'2016-08-11 07:54:29.710' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (595, N'SAN CAYETANO', 14, CAST(N'2016-08-11 07:54:29.710' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (596, N'SAN FRANCISCO', 14, CAST(N'2016-08-11 07:54:29.710' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (597, N'SAN JUAN DE RIOSECO', 14, CAST(N'2016-08-11 07:54:29.713' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (598, N'SASAIMA', 14, CAST(N'2016-08-11 07:54:29.713' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (599, N'SESQUILÉ', 14, CAST(N'2016-08-11 07:54:29.713' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (600, N'SIBATÉ', 14, CAST(N'2016-08-11 07:54:29.713' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (601, N'SILVANIA', 14, CAST(N'2016-08-11 07:54:29.713' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (602, N'SIMIJACA', 14, CAST(N'2016-08-11 07:54:29.713' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (603, N'SOACHA', 14, CAST(N'2016-08-11 07:54:29.713' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (604, N'SOPO', 14, CAST(N'2016-08-11 07:54:29.713' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (605, N'SUBACHOQUE', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (606, N'SUESCA', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (607, N'SUPATÁ', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (608, N'SUSA', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (609, N'SUTATAUSA', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (610, N'TABIO', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (611, N'TAUSA', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (612, N'TENA', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (613, N'TENJO', 14, CAST(N'2016-08-11 07:54:29.717' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (614, N'TIBACUY', 14, CAST(N'2016-08-11 07:54:29.720' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (615, N'TIBIRITA', 14, CAST(N'2016-08-11 07:54:29.720' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (616, N'TOCAIMA', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (617, N'TOCANCIPÁ', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (618, N'TOPAIPÍ', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (619, N'UBALÁ', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (620, N'UBAQUE', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (621, N'UBATÉ', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (622, N'UNE', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (623, N'UTICA', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (624, N'VERGARA', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (625, N'VIANI', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (626, N'VILLA GOMEZ', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (627, N'VILLA PINZÓN', 14, CAST(N'2016-08-11 07:54:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (628, N'VILLETA', 14, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (629, N'VIOTA', 14, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (630, N'YACOPÍ', 14, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (631, N'ZIPACÓN', 14, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (632, N'ZIPAQUIRÁ', 14, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (633, N'BARRANCO MINAS', 15, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (634, N'CACAHUAL', 15, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (635, N'INÍRIDA', 15, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (636, N'LA GUADALUPE', 15, CAST(N'2016-08-11 07:54:29.753' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (637, N'MAPIRIPANA', 15, CAST(N'2016-08-11 07:54:29.757' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (638, N'MORICHAL', 15, CAST(N'2016-08-11 07:54:29.757' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (639, N'PANA PANA', 15, CAST(N'2016-08-11 07:54:29.757' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (640, N'PUERTO COLOMBIA', 15, CAST(N'2016-08-11 07:54:29.757' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (641, N'SAN FELIPE', 15, CAST(N'2016-08-11 07:54:29.757' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (642, N'CALAMAR', 16, CAST(N'2016-08-11 07:54:29.757' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (643, N'EL RETORNO', 16, CAST(N'2016-08-11 07:54:29.757' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (644, N'MIRAFLOREZ', 16, CAST(N'2016-08-11 07:54:29.757' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (645, N'SAN JOSÉ DEL GUAVIARE', 16, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (646, N'ACEVEDO', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (647, N'AGRADO', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (648, N'AIPE', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (649, N'ALGECIRAS', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (650, N'ALTAMIRA', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (651, N'BARAYA', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (652, N'CAMPO ALEGRE', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (653, N'COLOMBIA', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (654, N'ELIAS', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (655, N'GARZÓN', 17, CAST(N'2016-08-11 07:54:29.760' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (656, N'GIGANTE', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (657, N'GUADALUPE', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (658, N'HOBO', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (659, N'IQUIRA', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (660, N'ISNOS', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (661, N'LA ARGENTINA', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (662, N'LA PLATA', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (663, N'NATAGA', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (664, N'NEIVA', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (665, N'OPORAPA', 17, CAST(N'2016-08-11 07:54:29.763' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (666, N'PAICOL', 17, CAST(N'2016-08-11 07:54:29.767' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (667, N'PALERMO', 17, CAST(N'2016-08-11 07:54:29.767' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (668, N'PALESTINA', 17, CAST(N'2016-08-11 07:54:29.767' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (669, N'PITAL', 17, CAST(N'2016-08-11 07:54:29.767' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (670, N'PITALITO', 17, CAST(N'2016-08-11 07:54:29.767' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (671, N'RIVERA', 17, CAST(N'2016-08-11 07:54:29.767' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (672, N'SALADO BLANCO', 17, CAST(N'2016-08-11 07:54:29.767' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (673, N'SAN AGUSTÍN', 17, CAST(N'2016-08-11 07:54:29.770' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (674, N'SANTA MARIA', 17, CAST(N'2016-08-11 07:54:29.770' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (675, N'SUAZA', 17, CAST(N'2016-08-11 07:54:29.770' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (676, N'TARQUI', 17, CAST(N'2016-08-11 07:54:29.770' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (677, N'TELLO', 17, CAST(N'2016-08-11 07:54:29.770' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (678, N'TERUEL', 17, CAST(N'2016-08-11 07:54:29.770' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (679, N'TESALIA', 17, CAST(N'2016-08-11 07:54:29.770' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (680, N'TIMANA', 17, CAST(N'2016-08-11 07:54:29.773' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (681, N'VILLAVIEJA', 17, CAST(N'2016-08-11 07:54:29.773' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (682, N'YAGUARA', 17, CAST(N'2016-08-11 07:54:29.773' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (683, N'ALBANIA', 18, CAST(N'2016-08-11 07:54:29.773' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (684, N'BARRANCAS', 18, CAST(N'2016-08-11 07:54:29.773' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (685, N'DIBULLA', 18, CAST(N'2016-08-11 07:54:29.773' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (686, N'DISTRACCIÓN', 18, CAST(N'2016-08-11 07:54:29.773' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (687, N'EL MOLINO', 18, CAST(N'2016-08-11 07:54:29.773' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (688, N'FONSECA', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (689, N'HATO NUEVO', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (690, N'LA JAGUA DEL PILAR', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (691, N'MAICAO', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (692, N'MANAURE', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (693, N'RIOHACHA', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (694, N'SAN JUAN DEL CESAR', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (695, N'URIBIA', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (696, N'URUMITA', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (697, N'VILLANUEVA', 18, CAST(N'2016-08-11 07:54:29.777' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (698, N'ALGARROBO', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (699, N'ARACATACA', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (700, N'ARIGUANI', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (701, N'CERRO SAN ANTONIO', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (702, N'CHIVOLO', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (703, N'CIENAGA', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (704, N'CONCORDIA', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (705, N'EL BANCO', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (706, N'EL PIÑON', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (707, N'EL RETEN', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (708, N'FUNDACION', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (709, N'GUAMAL', 19, CAST(N'2016-08-11 07:54:29.780' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (710, N'NUEVA GRANADA', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (711, N'PEDRAZA', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (712, N'PIJIÑO DEL CARMEN', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (713, N'PIVIJAY', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (714, N'PLATO', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (715, N'PUEBLO VIEJO', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (716, N'REMOLINO', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (717, N'SABANAS DE SAN ANGEL', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (718, N'SALAMINA', 19, CAST(N'2016-08-11 07:54:29.783' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (719, N'SAN SEBASTIAN DE BUENAVISTA', 19, CAST(N'2016-08-11 07:54:29.787' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (720, N'SAN ZENON', 19, CAST(N'2016-08-11 07:54:29.787' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (721, N'SANTA ANA', 19, CAST(N'2016-08-11 07:54:29.787' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (722, N'SANTA BARBARA DE PINTO', 19, CAST(N'2016-08-11 07:54:29.787' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (723, N'SANTA MARTA', 19, CAST(N'2016-08-11 07:54:29.787' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (724, N'SITIONUEVO', 19, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (725, N'TENERIFE', 19, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (726, N'ZAPAYAN', 19, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (727, N'ZONA BANANERA', 19, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (728, N'ACACIAS', 20, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (729, N'BARRANCA DE UPIA', 20, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (730, N'CABUYARO', 20, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (731, N'CASTILLA LA NUEVA', 20, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (732, N'CUBARRAL', 20, CAST(N'2016-08-11 07:54:29.790' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (733, N'CUMARAL', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (734, N'EL CALVARIO', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (735, N'EL CASTILLO', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (736, N'EL DORADO', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (737, N'FUENTE DE ORO', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (738, N'GRANADA', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (739, N'GUAMAL', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (740, N'LA MACARENA', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (741, N'LA URIBE', 20, CAST(N'2016-08-11 07:54:29.843' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (742, N'LEJANÍAS', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (743, N'MAPIRIPÁN', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (744, N'MESETAS', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (745, N'PUERTO CONCORDIA', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (746, N'PUERTO GAITÁN', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (747, N'PUERTO LLERAS', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (748, N'PUERTO LÓPEZ', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (749, N'PUERTO RICO', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (750, N'RESTREPO', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (751, N'SAN  JUAN DE ARAMA', 20, CAST(N'2016-08-11 07:54:29.847' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (752, N'SAN CARLOS GUAROA', 20, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (753, N'SAN JUANITO', 20, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (754, N'SAN MARTÍN', 20, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (755, N'VILLAVICENCIO', 20, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (756, N'VISTA HERMOSA', 20, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (757, N'ALBAN', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (758, N'ALDAÑA', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (759, N'ANCUYA', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (760, N'ARBOLEDA', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (761, N'BARBACOAS', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (762, N'BELEN', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (763, N'BUESACO', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (764, N'CHACHAGUI', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (765, N'COLON (GENOVA)', 21, CAST(N'2016-08-11 07:54:29.850' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (766, N'CONSACA', 21, CAST(N'2016-08-11 07:54:29.853' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (767, N'CONTADERO', 21, CAST(N'2016-08-11 07:54:29.853' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (768, N'CORDOBA', 21, CAST(N'2016-08-11 07:54:29.857' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (769, N'CUASPUD', 21, CAST(N'2016-08-11 07:54:29.857' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (770, N'CUMBAL', 21, CAST(N'2016-08-11 07:54:29.857' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (771, N'CUMBITARA', 21, CAST(N'2016-08-11 07:54:29.860' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (772, N'EL CHARCO', 21, CAST(N'2016-08-11 07:54:29.860' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (773, N'EL PEÑOL', 21, CAST(N'2016-08-11 07:54:29.860' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (774, N'EL ROSARIO', 21, CAST(N'2016-08-11 07:54:29.860' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (775, N'EL TABLÓN', 21, CAST(N'2016-08-11 07:54:29.863' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (776, N'EL TAMBO', 21, CAST(N'2016-08-11 07:54:29.863' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (777, N'FUNES', 21, CAST(N'2016-08-11 07:54:29.863' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (778, N'GUACHUCAL', 21, CAST(N'2016-08-11 07:54:29.863' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (779, N'GUAITARILLA', 21, CAST(N'2016-08-11 07:54:29.863' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (780, N'GUALMATAN', 21, CAST(N'2016-08-11 07:54:29.867' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (781, N'ILES', 21, CAST(N'2016-08-11 07:54:29.867' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (782, N'IMUES', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (783, N'IPIALES', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (784, N'LA CRUZ', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (785, N'LA FLORIDA', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (786, N'LA LLANADA', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (787, N'LA TOLA', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (788, N'LA UNION', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (789, N'LEIVA', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (790, N'LINARES', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (791, N'LOS ANDES', 21, CAST(N'2016-08-11 07:54:29.870' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (792, N'MAGUI', 21, CAST(N'2016-08-11 07:54:29.873' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (793, N'MALLAMA', 21, CAST(N'2016-08-11 07:54:29.877' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (794, N'MOSQUEZA', 21, CAST(N'2016-08-11 07:54:29.877' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (795, N'NARIÑO', 21, CAST(N'2016-08-11 07:54:29.880' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (796, N'OLAYA HERRERA', 21, CAST(N'2016-08-11 07:54:29.880' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (797, N'OSPINA', 21, CAST(N'2016-08-11 07:54:29.880' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (798, N'PASTO', 21, CAST(N'2016-08-11 07:54:29.880' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (799, N'PIZARRO', 21, CAST(N'2016-08-11 07:54:29.883' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (800, N'POLICARPA', 21, CAST(N'2016-08-11 07:54:29.883' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (801, N'POTOSI', 21, CAST(N'2016-08-11 07:54:29.887' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (802, N'PROVIDENCIA', 21, CAST(N'2016-08-11 07:54:29.890' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (803, N'PUERRES', 21, CAST(N'2016-08-11 07:54:29.893' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (804, N'PUPIALES', 21, CAST(N'2016-08-11 07:54:29.893' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (805, N'RICAURTE', 21, CAST(N'2016-08-11 07:54:29.893' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (806, N'ROBERTO PAYAN', 21, CAST(N'2016-08-11 07:54:29.897' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (807, N'SAMANIEGO', 21, CAST(N'2016-08-11 07:54:29.897' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (808, N'SAN BERNARDO', 21, CAST(N'2016-08-11 07:54:29.897' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (809, N'SAN LORENZO', 21, CAST(N'2016-08-11 07:54:29.900' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (810, N'SAN PABLO', 21, CAST(N'2016-08-11 07:54:29.900' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (811, N'SAN PEDRO DE CARTAGO', 21, CAST(N'2016-08-11 07:54:29.900' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (812, N'SANDONA', 21, CAST(N'2016-08-11 07:54:29.900' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (813, N'SANTA BARBARA', 21, CAST(N'2016-08-11 07:54:29.903' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (814, N'SANTACRUZ', 21, CAST(N'2016-08-11 07:54:29.903' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (815, N'SAPUYES', 21, CAST(N'2016-08-11 07:54:29.903' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (816, N'TAMINANGO', 21, CAST(N'2016-08-11 07:54:29.903' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (817, N'TANGUA', 21, CAST(N'2016-08-11 07:54:29.903' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (818, N'TUMACO', 21, CAST(N'2016-08-11 07:54:29.907' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (819, N'TUQUERRES', 21, CAST(N'2016-08-11 07:54:29.907' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (820, N'YACUANQUER', 21, CAST(N'2016-08-11 07:54:29.910' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (821, N'ABREGO', 22, CAST(N'2016-08-11 07:54:29.910' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (822, N'ARBOLEDAS', 22, CAST(N'2016-08-11 07:54:29.910' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (823, N'BOCHALEMA', 22, CAST(N'2016-08-11 07:54:29.910' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (824, N'BUCARASICA', 22, CAST(N'2016-08-11 07:54:29.910' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (825, N'CÁCHIRA', 22, CAST(N'2016-08-11 07:54:29.913' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (826, N'CÁCOTA', 22, CAST(N'2016-08-11 07:54:29.913' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (827, N'CHINÁCOTA', 22, CAST(N'2016-08-11 07:54:29.917' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (828, N'CHITAGÁ', 22, CAST(N'2016-08-11 07:54:29.917' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (829, N'CONVENCIÓN', 22, CAST(N'2016-08-11 07:54:29.920' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (830, N'CÚCUTA', 22, CAST(N'2016-08-11 07:54:29.920' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (831, N'CUCUTILLA', 22, CAST(N'2016-08-11 07:54:29.920' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (832, N'DURANIA', 22, CAST(N'2016-08-11 07:54:29.923' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (833, N'EL CARMEN', 22, CAST(N'2016-08-11 07:54:29.927' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (834, N'EL TARRA', 22, CAST(N'2016-08-11 07:54:29.927' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (835, N'EL ZULIA', 22, CAST(N'2016-08-11 07:54:29.927' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (836, N'GRAMALOTE', 22, CAST(N'2016-08-11 07:54:29.930' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (837, N'HACARI', 22, CAST(N'2016-08-11 07:54:29.933' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (838, N'HERRÁN', 22, CAST(N'2016-08-11 07:54:29.933' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (839, N'LA ESPERANZA', 22, CAST(N'2016-08-11 07:54:29.937' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (840, N'LA PLAYA', 22, CAST(N'2016-08-11 07:54:29.937' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (841, N'LABATECA', 22, CAST(N'2016-08-11 07:54:29.940' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (842, N'LOS PATIOS', 22, CAST(N'2016-08-11 07:54:29.940' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (843, N'LOURDES', 22, CAST(N'2016-08-11 07:54:29.940' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (844, N'MUTISCUA', 22, CAST(N'2016-08-11 07:54:29.943' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (845, N'OCAÑA', 22, CAST(N'2016-08-11 07:54:29.943' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (846, N'PAMPLONA', 22, CAST(N'2016-08-11 07:54:29.947' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (847, N'PAMPLONITA', 22, CAST(N'2016-08-11 07:54:29.947' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (848, N'PUERTO SANTANDER', 22, CAST(N'2016-08-11 07:54:29.947' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (849, N'RAGONVALIA', 22, CAST(N'2016-08-11 07:54:29.947' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (850, N'SALAZAR', 22, CAST(N'2016-08-11 07:54:29.950' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (851, N'SAN CALIXTO', 22, CAST(N'2016-08-11 07:54:29.950' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (852, N'SAN CAYETANO', 22, CAST(N'2016-08-11 07:54:29.950' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (853, N'SANTIAGO', 22, CAST(N'2016-08-11 07:54:29.950' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (854, N'SARDINATA', 22, CAST(N'2016-08-11 07:54:29.950' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (855, N'SILOS', 22, CAST(N'2016-08-11 07:54:29.953' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (856, N'TEORAMA', 22, CAST(N'2016-08-11 07:54:29.953' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (857, N'TIBÚ', 22, CAST(N'2016-08-11 07:54:29.953' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (858, N'TOLEDO', 22, CAST(N'2016-08-11 07:54:29.953' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (859, N'VILLA CARO', 22, CAST(N'2016-08-11 07:54:29.957' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (860, N'VILLA DEL ROSARIO', 22, CAST(N'2016-08-11 07:54:29.957' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (861, N'COLÓN', 23, CAST(N'2016-08-11 07:54:29.960' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (862, N'MOCOA', 23, CAST(N'2016-08-11 07:54:29.960' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (863, N'ORITO', 23, CAST(N'2016-08-11 07:54:29.963' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (864, N'PUERTO ASÍS', 23, CAST(N'2016-08-11 07:54:29.963' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (865, N'PUERTO CAYCEDO', 23, CAST(N'2016-08-11 07:54:29.963' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (866, N'PUERTO GUZMÁN', 23, CAST(N'2016-08-11 07:54:29.963' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (867, N'PUERTO LEGUÍZAMO', 23, CAST(N'2016-08-11 07:54:29.963' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (868, N'SAN FRANCISCO', 23, CAST(N'2016-08-11 07:54:29.967' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (869, N'SAN MIGUEL', 23, CAST(N'2016-08-11 07:54:29.967' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (870, N'SANTIAGO', 23, CAST(N'2016-08-11 07:54:29.967' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (871, N'SIBUNDOY', 23, CAST(N'2016-08-11 07:54:29.967' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (872, N'VALLE DEL GUAMUEZ', 23, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (873, N'VILLAGARZÓN', 23, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (874, N'ARMENIA', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (875, N'BUENAVISTA', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (876, N'CALARCÁ', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (877, N'CIRCASIA', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (878, N'CÓRDOBA', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (879, N'FILANDIA', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (880, N'GÉNOVA', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (881, N'LA TEBAIDA', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (882, N'MONTENEGRO', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (883, N'PIJAO', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (884, N'QUIMBAYA', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (885, N'SALENTO', 24, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (886, N'APIA', 25, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (887, N'BALBOA', 25, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (888, N'BELÉN DE UMBRÍA', 25, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (889, N'DOS QUEBRADAS', 25, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (890, N'GUATICA', 25, CAST(N'2016-08-11 07:54:29.970' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (891, N'LA CELIA', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (892, N'LA VIRGINIA', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (893, N'MARSELLA', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (894, N'MISTRATO', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (895, N'PEREIRA', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (896, N'PUEBLO RICO', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (897, N'QUINCHÍA', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (898, N'SANTA ROSA DE CABAL', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (899, N'SANTUARIO', 25, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (900, N'PROVIDENCIA', 26, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (901, N'SAN ANDRES', 26, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (902, N'SANTA CATALINA', 26, CAST(N'2016-08-11 07:54:29.973' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (903, N'AGUADA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (904, N'ALBANIA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (905, N'ARATOCA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (906, N'BARBOSA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (907, N'BARICHARA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (908, N'BARRANCABERMEJA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (909, N'BETULIA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (910, N'BOLÍVAR', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (911, N'BUCARAMANGA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (912, N'CABRERA', 27, CAST(N'2016-08-11 07:54:29.977' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (913, N'CALIFORNIA', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (914, N'CAPITANEJO', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (915, N'CARCASI', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (916, N'CEPITA', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (917, N'CERRITO', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (918, N'CHARALÁ', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (919, N'CHARTA', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (920, N'CHIMA', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (921, N'CHIPATÁ', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (922, N'CIMITARRA', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (923, N'CONCEPCIÓN', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (924, N'CONFINES', 27, CAST(N'2016-08-11 07:54:29.980' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (925, N'CONTRATACIÓN', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (926, N'COROMORO', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (927, N'CURITÍ', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (928, N'EL CARMEN', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (929, N'EL GUACAMAYO', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (930, N'EL PEÑÓN', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (931, N'EL PLAYÓN', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (932, N'ENCINO', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (933, N'ENCISO', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (934, N'FLORIÁN', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (935, N'FLORIDABLANCA', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (936, N'GALÁN', 27, CAST(N'2016-08-11 07:54:29.983' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (937, N'GAMBITA', 27, CAST(N'2016-08-11 07:54:29.987' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (938, N'GIRÓN', 27, CAST(N'2016-08-11 07:54:29.987' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (939, N'GUACA', 27, CAST(N'2016-08-11 07:54:29.987' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (940, N'GUADALUPE', 27, CAST(N'2016-08-11 07:54:29.987' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (941, N'GUAPOTA', 27, CAST(N'2016-08-11 07:54:29.987' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (942, N'GUAVATÁ', 27, CAST(N'2016-08-11 07:54:29.987' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (943, N'GUEPSA', 27, CAST(N'2016-08-11 07:54:29.987' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (944, N'HATO', 27, CAST(N'2016-08-11 07:54:29.987' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (945, N'JESÚS MARIA', 27, CAST(N'2016-08-11 07:54:30.240' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (946, N'JORDÁN', 27, CAST(N'2016-08-11 07:54:30.240' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (947, N'LA BELLEZA', 27, CAST(N'2016-08-11 07:54:30.243' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (948, N'LA PAZ', 27, CAST(N'2016-08-11 07:54:30.243' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (949, N'LANDAZURI', 27, CAST(N'2016-08-11 07:54:30.243' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (950, N'LEBRIJA', 27, CAST(N'2016-08-11 07:54:30.243' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (951, N'LOS SANTOS', 27, CAST(N'2016-08-11 07:54:30.243' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (952, N'MACARAVITA', 27, CAST(N'2016-08-11 07:54:30.247' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (953, N'MÁLAGA', 27, CAST(N'2016-08-11 07:54:30.247' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (954, N'MATANZA', 27, CAST(N'2016-08-11 07:54:30.247' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (955, N'MOGOTES', 27, CAST(N'2016-08-11 07:54:30.247' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (956, N'MOLAGAVITA', 27, CAST(N'2016-08-11 07:54:30.247' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (957, N'OCAMONTE', 27, CAST(N'2016-08-11 07:54:30.247' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (958, N'OIBA', 27, CAST(N'2016-08-11 07:54:30.247' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (959, N'ONZAGA', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (960, N'PALMAR', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (961, N'PALMAS DEL SOCORRO', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (962, N'PÁRAMO', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (963, N'PIEDECUESTA', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (964, N'PINCHOTE', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (965, N'PUENTE NACIONAL', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (966, N'PUERTO PARRA', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (967, N'PUERTO WILCHES', 27, CAST(N'2016-08-11 07:54:30.250' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (968, N'RIONEGRO', 27, CAST(N'2016-08-11 07:54:30.253' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (969, N'SABANA DE TORRES', 27, CAST(N'2016-08-11 07:54:30.253' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (970, N'SAN ANDRÉS', 27, CAST(N'2016-08-11 07:54:30.253' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (971, N'SAN BENITO', 27, CAST(N'2016-08-11 07:54:30.253' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (972, N'SAN GIL', 27, CAST(N'2016-08-11 07:54:30.257' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (973, N'SAN JOAQUÍN', 27, CAST(N'2016-08-11 07:54:30.257' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (974, N'SAN JOSÉ DE MIRANDA', 27, CAST(N'2016-08-11 07:54:30.260' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (975, N'SAN MIGUEL', 27, CAST(N'2016-08-11 07:54:30.260' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (976, N'SAN VICENTE DE CHUCURÍ', 27, CAST(N'2016-08-11 07:54:30.260' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (977, N'SANTA BÁRBARA', 27, CAST(N'2016-08-11 07:54:30.260' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (978, N'SANTA HELENA', 27, CAST(N'2016-08-11 07:54:30.260' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (979, N'SIMACOTA', 27, CAST(N'2016-08-11 07:54:30.260' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (980, N'SOCORRO', 27, CAST(N'2016-08-11 07:54:30.260' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (981, N'SUAITA', 27, CAST(N'2016-08-11 07:54:30.263' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (982, N'SUCRE', 27, CAST(N'2016-08-11 07:54:30.263' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (983, N'SURATA', 27, CAST(N'2016-08-11 07:54:30.263' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (984, N'TONA', 27, CAST(N'2016-08-11 07:54:30.263' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (985, N'VALLE SAN JOSÉ', 27, CAST(N'2016-08-11 07:54:30.263' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (986, N'VÉLEZ', 27, CAST(N'2016-08-11 07:54:30.263' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (987, N'VETAS', 27, CAST(N'2016-08-11 07:54:30.267' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (988, N'VILLANUEVA', 27, CAST(N'2016-08-11 07:54:30.267' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (989, N'ZAPATOCA', 27, CAST(N'2016-08-11 07:54:30.267' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (990, N'BUENAVISTA', 28, CAST(N'2016-08-11 07:54:30.267' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (991, N'CAIMITO', 28, CAST(N'2016-08-11 07:54:30.267' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (992, N'CHALÁN', 28, CAST(N'2016-08-11 07:54:30.267' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (993, N'COLOSO', 28, CAST(N'2016-08-11 07:54:30.267' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (994, N'COROZAL', 28, CAST(N'2016-08-11 07:54:30.270' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (995, N'EL ROBLE', 28, CAST(N'2016-08-11 07:54:30.270' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (996, N'GALERAS', 28, CAST(N'2016-08-11 07:54:30.270' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (997, N'GUARANDA', 28, CAST(N'2016-08-11 07:54:30.270' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (998, N'LA UNIÓN', 28, CAST(N'2016-08-11 07:54:30.270' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (999, N'LOS PALMITOS', 28, CAST(N'2016-08-11 07:54:30.273' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1000, N'MAJAGUAL', 28, CAST(N'2016-08-11 07:54:30.273' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1001, N'MORROA', 28, CAST(N'2016-08-11 07:54:30.273' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1002, N'OVEJAS', 28, CAST(N'2016-08-11 07:54:30.277' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1003, N'PALMITO', 28, CAST(N'2016-08-11 07:54:30.277' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1004, N'SAMPUES', 28, CAST(N'2016-08-11 07:54:30.277' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1005, N'SAN BENITO ABAD', 28, CAST(N'2016-08-11 07:54:30.277' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1006, N'SAN JUAN DE BETULIA', 28, CAST(N'2016-08-11 07:54:30.277' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1007, N'SAN MARCOS', 28, CAST(N'2016-08-11 07:54:30.277' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1008, N'SAN ONOFRE', 28, CAST(N'2016-08-11 07:54:30.277' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1009, N'SAN PEDRO', 28, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1010, N'SINCÉ', 28, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1011, N'SINCELEJO', 28, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1012, N'SUCRE', 28, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1013, N'TOLÚ', 28, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1014, N'TOLUVIEJO', 28, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1015, N'ALPUJARRA', 29, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1016, N'ALVARADO', 29, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1017, N'AMBALEMA', 29, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1018, N'ANZOATEGUI', 29, CAST(N'2016-08-11 07:54:30.280' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1019, N'ARMERO (GUAYABAL)', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1020, N'ATACO', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1021, N'CAJAMARCA', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1022, N'CARMEN DE APICALÁ', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1023, N'CASABIANCA', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1024, N'CHAPARRAL', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1025, N'COELLO', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1026, N'COYAIMA', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1027, N'CUNDAY', 29, CAST(N'2016-08-11 07:54:30.283' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1028, N'DOLORES', 29, CAST(N'2016-08-11 07:54:30.287' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1029, N'ESPINAL', 29, CAST(N'2016-08-11 07:54:30.287' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1030, N'FALÁN', 29, CAST(N'2016-08-11 07:54:30.287' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1031, N'FLANDES', 29, CAST(N'2016-08-11 07:54:30.287' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1032, N'FRESNO', 29, CAST(N'2016-08-11 07:54:30.287' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1033, N'GUAMO', 29, CAST(N'2016-08-11 07:54:30.287' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1034, N'HERVEO', 29, CAST(N'2016-08-11 07:54:30.290' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1035, N'HONDA', 29, CAST(N'2016-08-11 07:54:30.290' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1036, N'IBAGUÉ', 29, CAST(N'2016-08-11 07:54:30.290' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1037, N'ICONONZO', 29, CAST(N'2016-08-11 07:54:30.290' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1038, N'LÉRIDA', 29, CAST(N'2016-08-11 07:54:30.290' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1039, N'LÍBANO', 29, CAST(N'2016-08-11 07:54:30.293' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1040, N'MARIQUITA', 29, CAST(N'2016-08-11 07:54:30.293' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1041, N'MELGAR', 29, CAST(N'2016-08-11 07:54:30.293' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1042, N'MURILLO', 29, CAST(N'2016-08-11 07:54:30.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1043, N'NATAGAIMA', 29, CAST(N'2016-08-11 07:54:30.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1044, N'ORTEGA', 29, CAST(N'2016-08-11 07:54:30.343' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1045, N'PALOCABILDO', 29, CAST(N'2016-08-11 07:54:30.417' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1046, N'PIEDRAS PLANADAS', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1047, N'PRADO', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1048, N'PURIFICACIÓN', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1049, N'RIOBLANCO', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1050, N'RONCESVALLES', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1051, N'ROVIRA', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1052, N'SALDAÑA', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1053, N'SAN ANTONIO', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1054, N'SAN LUIS', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1055, N'SANTA ISABEL', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1056, N'SUÁREZ', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1057, N'VALLE DE SAN JUAN', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1058, N'VENADILLO', 29, CAST(N'2016-08-11 07:54:30.420' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1059, N'VILLAHERMOSA', 29, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1060, N'VILLARRICA', 29, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1061, N'ALCALÁ', 30, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1062, N'ANDALUCÍA', 30, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1063, N'ANSERMA NUEVO', 30, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1064, N'ARGELIA', 30, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1065, N'BOLÍVAR', 30, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1066, N'BUENAVENTURA', 30, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1067, N'BUGA', 30, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1068, N'BUGALAGRANDE', 30, CAST(N'2016-08-11 07:54:30.423' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1069, N'CAICEDONIA', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1070, N'CALI', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1071, N'CALIMA (DARIEN)', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1072, N'CANDELARIA', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1073, N'CARTAGO', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1074, N'DAGUA', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1075, N'EL AGUILA', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1076, N'EL CAIRO', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1077, N'EL CERRITO', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1078, N'EL DOVIO', 30, CAST(N'2016-08-11 07:54:30.427' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1079, N'FLORIDA', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1080, N'GINEBRA GUACARI', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1081, N'JAMUNDÍ', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1082, N'LA CUMBRE', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1083, N'LA UNIÓN', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1084, N'LA VICTORIA', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1085, N'OBANDO', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1086, N'PALMIRA', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1087, N'PRADERA', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1088, N'RESTREPO', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1089, N'RIO FRÍO', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1090, N'ROLDANILLO', 30, CAST(N'2016-08-11 07:54:30.430' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1091, N'SAN PEDRO', 30, CAST(N'2016-08-11 07:54:30.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1092, N'SEVILLA', 30, CAST(N'2016-08-11 07:54:30.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1093, N'TORO', 30, CAST(N'2016-08-11 07:54:30.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1094, N'TRUJILLO', 30, CAST(N'2016-08-11 07:54:30.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1095, N'TULÚA', 30, CAST(N'2016-08-11 07:54:30.433' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1096, N'ULLOA', 30, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1097, N'VERSALLES', 30, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1098, N'VIJES', 30, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1099, N'YOTOCO', 30, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1100, N'YUMBO', 30, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1101, N'ZARZAL', 30, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1102, N'CARURÚ', 31, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1103, N'MITÚ', 31, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1104, N'PACOA', 31, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1105, N'PAPUNAUA', 31, CAST(N'2016-08-11 07:54:30.437' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1106, N'TARAIRA', 31, CAST(N'2016-08-11 07:54:30.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1107, N'YAVARATÉ', 31, CAST(N'2016-08-11 07:54:30.440' AS DateTime), NULL)
GO
INSERT [dbo].[ciudades] ([id], [nombre], [iddepartamento], [fechacreacion], [codigo]) VALUES (1115, N'EL ENCANTO', 1, CAST(N'2016-08-11 07:54:29.300' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[ciudades] OFF
GO
SET IDENTITY_INSERT [dbo].[clientes] ON 

GO
INSERT [dbo].[clientes] ([id], [tipoPersona_id], [tipoDocumento_id], [identificacion], [primerNombre], [segundoNombre], [primerApellido], [segundoApellido], [fechaNacimiento], [genero_id], [email], [celular], [telefonoCasa], [telefonoOficina], [jornadaEntrega], [idCreatorby], [dateCreated], [idRetiredby], [dateRetired], [idChangedby], [dateChanged], [activo]) VALUES (2, 1, 1, N'8255', N'ghjkgh', NULL, N'hkljhkj', NULL, CAST(N'2018-07-16 14:33:00' AS SmallDateTime), 1, N'jhgkj@gmail.com', N'jihuygjhg', NULL, NULL, N'Mañana', 1, CAST(N'2018-07-16' AS Date), NULL, NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[clientes] OFF
GO
SET IDENTITY_INSERT [dbo].[departamentos] ON 

GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (1, N'ANTIOQUIA', 1, CAST(N'2016-08-11 07:54:17.793' AS DateTime), N'01')
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (2, N'ARAUCA', 1, CAST(N'2016-08-11 07:54:17.797' AS DateTime), N'20')
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (3, N'ATLÁNTICO', 1, CAST(N'2016-08-11 07:54:17.797' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (4, N'BOLÍVAR', 1, CAST(N'2016-08-11 07:54:17.797' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (5, N'BOYACÁ', 1, CAST(N'2016-08-11 07:54:17.797' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (6, N'CALDAS', 1, CAST(N'2016-08-11 07:54:17.797' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (7, N'CAQUETÁ', 1, CAST(N'2016-08-11 07:54:17.797' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (8, N'CASANARE', 1, CAST(N'2016-08-11 07:54:17.797' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (9, N'CAUCA', 1, CAST(N'2016-08-11 07:54:17.797' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (10, N'CESAR', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (11, N'CHOCÓ', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (12, N'CÓRDOBA', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (13, N'CUNDINAMARCA', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (14, N'GUAINÍA', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (15, N'GUAVIARE', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (16, N'HUILA', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (17, N'LA GUAJIRA', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (18, N'MAGDALENA', 1, CAST(N'2016-08-11 07:54:17.800' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (19, N'META', 1, CAST(N'2016-08-11 07:54:17.803' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (20, N'NARIÑO', 1, CAST(N'2016-08-11 07:54:17.803' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (21, N'NORTE DE SANTANDER', 1, CAST(N'2016-08-11 07:54:17.803' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (22, N'PUTUMAYO', 1, CAST(N'2016-08-11 07:54:17.803' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (23, N'QUINDÍO', 1, CAST(N'2016-08-11 07:54:17.803' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (24, N'RISARALDA', 1, CAST(N'2016-08-11 07:54:17.803' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (25, N'SAN ANDRÉS Y PROVIDENCIA', 1, CAST(N'2016-08-11 07:54:17.803' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (26, N'SANTANDER', 1, CAST(N'2016-08-11 07:54:17.807' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (27, N'SUCRE', 1, CAST(N'2016-08-11 07:54:17.807' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (28, N'TOLIMA', 1, CAST(N'2016-08-11 07:54:17.807' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (29, N'VALLE DEL CAUCA', 1, CAST(N'2016-08-11 07:54:17.807' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (30, N'VAUPÉS', 1, CAST(N'2016-08-11 07:54:17.807' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (31, N'VICHADA', 1, CAST(N'2016-08-11 07:54:17.807' AS DateTime), NULL)
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (32, N'Prueba', 1, CAST(N'2016-12-19 19:52:10.853' AS DateTime), N'10')
GO
INSERT [dbo].[departamentos] ([id], [nombre], [idpais], [fechacreacion], [codigo]) VALUES (33, N'AMAZONAS', 1, CAST(N'2016-08-11 07:54:17.793' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[departamentos] OFF
GO
SET IDENTITY_INSERT [dbo].[empresarios] ON 

GO
INSERT [dbo].[empresarios] ([id], [tipoEmpresa_id], [razonSocial], [tipoDocumento_id], [identificacion], [tipoPersona_id], [direccion], [ciudad_id], [telefono], [celular], [emailNotificacion], [numeroRegistroArtesEscenicas], [nombreRL], [apellidosRL], [tipoDocumentoRL_id], [identificacionRL], [sexo_idRL], [emailRL], [direccionRL], [ciudad_idRL], [telefonoRL], [celularRL], [ventasWebReacudoDirecto], [crateDate], [bloqueado]) VALUES (2, 1, N'Rason Socialista', 2, N'99999999', 2, N'Direccion', 1, N'teefono', N'Celular', N'Email@cc.cc', N'NumeroReistro', N'NombreRL', N'ApellidoRL', 1, N'identificacionRL', 1, N'CorreoRL@cc.cc', N'DireccionRL', 12, N'telefonoRL', N'CelularRL', 0, CAST(N'2016-12-29 19:32:00' AS SmallDateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[empresarios] OFF
GO
SET IDENTITY_INSERT [dbo].[EstadoImpresion] ON 

GO
INSERT [dbo].[EstadoImpresion] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (2, N'Impreso', N'Impreso', CAST(N'2017-09-05 23:08:01.763' AS DateTime))
GO
INSERT [dbo].[EstadoImpresion] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (3, N'Disponible', N'Disponible', CAST(N'2017-09-05 23:08:01.777' AS DateTime))
GO
INSERT [dbo].[EstadoImpresion] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (4, N'Impreso-Defectuoso', N'Impreso-Defectuoso', CAST(N'2017-09-05 23:08:01.777' AS DateTime))
GO
INSERT [dbo].[EstadoImpresion] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (5, N'Reimpreso', N'Reimpreso', CAST(N'2017-09-05 23:08:01.777' AS DateTime))
GO
INSERT [dbo].[EstadoImpresion] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (6, N'En-Cola', N'En-Cola', CAST(N'2018-02-04 20:26:04.460' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[EstadoImpresion] OFF
GO
SET IDENTITY_INSERT [dbo].[etapasVentaEvento] ON 

GO
INSERT [dbo].[etapasVentaEvento] ([id], [nombre], [descripcion], [tipo], [fechaInicial], [fechaFinal], [idevento], [LlevaCodigo], [Codigo]) VALUES (25, N'Lanzamiento', N'dssdhsdjkfjhsdf jk hsdjfh js dhfjk h', NULL, CAST(N'2018-05-26 00:00:00' AS SmallDateTime), CAST(N'2018-05-31 00:00:00' AS SmallDateTime), 68, 1, N'hhuiuiu667676jkjk')
GO
INSERT [dbo].[etapasVentaEvento] ([id], [nombre], [descripcion], [tipo], [fechaInicial], [fechaFinal], [idevento], [LlevaCodigo], [Codigo]) VALUES (26, N'Preventa 1', N'Prueba 2', NULL, CAST(N'2018-06-01 00:00:00' AS SmallDateTime), CAST(N'2018-06-07 00:00:00' AS SmallDateTime), 68, 0, N'')
GO
INSERT [dbo].[etapasVentaEvento] ([id], [nombre], [descripcion], [tipo], [fechaInicial], [fechaFinal], [idevento], [LlevaCodigo], [Codigo]) VALUES (27, N'Etapa Unica', N'123123123', NULL, CAST(N'2018-05-27 20:54:00' AS SmallDateTime), CAST(N'2018-06-29 00:00:00' AS SmallDateTime), 69, 1, N'12312312')
GO
INSERT [dbo].[etapasVentaEvento] ([id], [nombre], [descripcion], [tipo], [fechaInicial], [fechaFinal], [idevento], [LlevaCodigo], [Codigo]) VALUES (28, N'Lanzamiento', N'wqeqweqweqwe', NULL, CAST(N'2018-05-28 00:00:00' AS SmallDateTime), CAST(N'2018-06-02 00:00:00' AS SmallDateTime), 70, NULL, NULL)
GO
INSERT [dbo].[etapasVentaEvento] ([id], [nombre], [descripcion], [tipo], [fechaInicial], [fechaFinal], [idevento], [LlevaCodigo], [Codigo]) VALUES (29, N'Preventa 1', N'rwyretyery', NULL, CAST(N'2018-06-03 00:00:00' AS SmallDateTime), CAST(N'2018-07-07 00:00:00' AS SmallDateTime), 70, NULL, NULL)
GO
INSERT [dbo].[etapasVentaEvento] ([id], [nombre], [descripcion], [tipo], [fechaInicial], [fechaFinal], [idevento], [LlevaCodigo], [Codigo]) VALUES (30, N'PReventa # 2', N'sadassad', NULL, CAST(N'2018-07-08 00:00:00' AS SmallDateTime), CAST(N'2018-07-14 00:00:00' AS SmallDateTime), 70, NULL, NULL)
GO
INSERT [dbo].[etapasVentaEvento] ([id], [nombre], [descripcion], [tipo], [fechaInicial], [fechaFinal], [idevento], [LlevaCodigo], [Codigo]) VALUES (31, N'Etapa Final', N'334534534', NULL, CAST(N'2018-07-15 00:00:00' AS SmallDateTime), CAST(N'2018-07-21 00:00:00' AS SmallDateTime), 70, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[etapasVentaEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[eventos] ON 

GO
INSERT [dbo].[eventos] ([id], [nombre], [multifuncion], [cantidadFunciones], [fechaInicial], [fechaFinal], [empresario_id], [tipoEvento], [foto], [descripcion], [edadMinima], [eventoPublico], [tipoRecaudo_id], [entradasxPedidoOcompra], [entradasxUsuario], [aplicaServicio], [cantidadDiasValides], [fechaCreacion], [AforoTotal], [Estado], [urlVideo]) VALUES (68, N'BAUM PARK', 1, 1, CAST(N'2018-06-29 00:00:00' AS SmallDateTime), CAST(N'2018-06-30 00:00:00' AS SmallDateTime), 2, NULL, N'', N'<h1 style="text-align:center;">Oe Oe</h1>', 18, 0, 2, 2, 2, 0, 1, CAST(N'2018-05-25 20:49:00' AS SmallDateTime), 5000, N'tickets', N'http://localhost:63961/event')
GO
INSERT [dbo].[eventos] ([id], [nombre], [multifuncion], [cantidadFunciones], [fechaInicial], [fechaFinal], [empresario_id], [tipoEvento], [foto], [descripcion], [edadMinima], [eventoPublico], [tipoRecaudo_id], [entradasxPedidoOcompra], [entradasxUsuario], [aplicaServicio], [cantidadDiasValides], [fechaCreacion], [AforoTotal], [Estado], [urlVideo]) VALUES (69, N'Evento # 1', 1, 1, CAST(N'2018-06-29 00:00:00' AS SmallDateTime), CAST(N'2018-06-30 00:00:00' AS SmallDateTime), 2, NULL, N'', N'<h1 style="text-align:center;">Oe oe Oe&nbsp;</h1>', 18, 0, 1, 2, 2, 0, 1, CAST(N'2018-05-25 20:56:00' AS SmallDateTime), 5000, N'tickets', N'http://sebastianticketevento.appsource.com.co/event')
GO
INSERT [dbo].[eventos] ([id], [nombre], [multifuncion], [cantidadFunciones], [fechaInicial], [fechaFinal], [empresario_id], [tipoEvento], [foto], [descripcion], [edadMinima], [eventoPublico], [tipoRecaudo_id], [entradasxPedidoOcompra], [entradasxUsuario], [aplicaServicio], [cantidadDiasValides], [fechaCreacion], [AforoTotal], [Estado], [urlVideo]) VALUES (70, N'Evento # 1', 1, 2, CAST(N'2018-06-28 17:30:00' AS SmallDateTime), CAST(N'2018-07-30 23:30:00' AS SmallDateTime), 2, NULL, NULL, N'Descipci&oacute;n', 18, 0, 3, 12, 2, 0, 2, CAST(N'2018-05-27 12:20:00' AS SmallDateTime), 4996, N'finalizado', N'http://localhost:63961/event')
GO
SET IDENTITY_INSERT [dbo].[eventos] OFF
GO
SET IDENTITY_INSERT [dbo].[funcionesEventos] ON 

GO
INSERT [dbo].[funcionesEventos] ([id], [idevento], [fechaIncial], [fechaFinal], [descripcion], [label], [fechaCreacion], [aperturaPuertas]) VALUES (25, 68, CAST(N'2018-06-29 00:00:00' AS SmallDateTime), CAST(N'2018-06-30 00:00:00' AS SmallDateTime), N'qweqwe', N'Funcion Unica', CAST(N'2018-05-25 20:49:00' AS SmallDateTime), CAST(N'2018-05-25 01:30:00' AS SmallDateTime))
GO
INSERT [dbo].[funcionesEventos] ([id], [idevento], [fechaIncial], [fechaFinal], [descripcion], [label], [fechaCreacion], [aperturaPuertas]) VALUES (26, 69, CAST(N'2018-06-29 00:00:00' AS SmallDateTime), CAST(N'2018-06-30 00:00:00' AS SmallDateTime), N'123123', N'Funcion # 1', CAST(N'2018-05-25 20:56:00' AS SmallDateTime), CAST(N'2018-05-25 02:00:00' AS SmallDateTime))
GO
INSERT [dbo].[funcionesEventos] ([id], [idevento], [fechaIncial], [fechaFinal], [descripcion], [label], [fechaCreacion], [aperturaPuertas]) VALUES (27, 70, CAST(N'2018-06-28 17:30:00' AS SmallDateTime), CAST(N'2018-06-29 23:30:00' AS SmallDateTime), N'213213ersre', N'FUncion22', CAST(N'2018-05-27 12:20:00' AS SmallDateTime), CAST(N'2018-05-27 16:30:00' AS SmallDateTime))
GO
INSERT [dbo].[funcionesEventos] ([id], [idevento], [fechaIncial], [fechaFinal], [descripcion], [label], [fechaCreacion], [aperturaPuertas]) VALUES (28, 70, CAST(N'2018-07-29 17:30:00' AS SmallDateTime), CAST(N'2018-07-30 23:30:00' AS SmallDateTime), N'21312423423', N'sasdfsdf', CAST(N'2018-05-27 12:20:00' AS SmallDateTime), CAST(N'2018-05-27 16:30:00' AS SmallDateTime))
GO
SET IDENTITY_INSERT [dbo].[funcionesEventos] OFF
GO
SET IDENTITY_INSERT [dbo].[Genero] ON 

GO
INSERT [dbo].[Genero] ([id], [nombre], [fechacreacion]) VALUES (1, N'Hombre', CAST(N'2016-08-24 10:48:28.830' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Genero] OFF
GO
SET IDENTITY_INSERT [dbo].[Impuesto] ON 

GO
INSERT [dbo].[Impuesto] ([id], [nombre], [valor], [fechacreacion]) VALUES (1, N'IVA', CAST(16.000 AS Decimal(18, 3)), CAST(N'2016-08-24 10:48:56.713' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Impuesto] OFF
GO
SET IDENTITY_INSERT [dbo].[impuestosEvento] ON 

GO
INSERT [dbo].[impuestosEvento] ([id], [idevento], [idimpuesto]) VALUES (35, 68, 1)
GO
INSERT [dbo].[impuestosEvento] ([id], [idevento], [idimpuesto]) VALUES (36, 69, 1)
GO
INSERT [dbo].[impuestosEvento] ([id], [idevento], [idimpuesto]) VALUES (37, 70, 1)
GO
SET IDENTITY_INSERT [dbo].[impuestosEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[localidad] ON 

GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (89, N'Localidad # 1', 3000, 1, CAST(N'2018-05-25 20:49:00' AS SmallDateTime), 1, NULL, 1, NULL, N'Cereveza', 20000.0000, 0, 0, NULL, NULL)
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (90, N'FIlas y Sillas', 200, 2, CAST(N'2018-05-25 20:49:00' AS SmallDateTime), NULL, NULL, 0, NULL, N'Cerveza', 23000.0000, 20, 10, N'Up-Down', N'R-L')
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (91, N'Mesas y Sillas', 300, 3, CAST(N'2018-05-25 20:49:00' AS SmallDateTime), NULL, NULL, 0, NULL, N'Consumo', 21312.0000, 30, 10, NULL, NULL)
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (92, N'Plco', 750, 4, CAST(N'2018-05-25 20:49:00' AS SmallDateTime), 1, NULL, 1, NULL, N'Botella de Old Parr', 1000000.0000, 50, 15, NULL, NULL)
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (93, N'VIP', 750, 1, CAST(N'2018-05-25 20:49:00' AS SmallDateTime), NULL, NULL, 0, NULL, N'Verveza', 10000.0000, 0, 0, NULL, NULL)
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (94, N'Localidad # 1', 5000, 1, CAST(N'2018-05-25 20:56:00' AS SmallDateTime), 123, NULL, 1, NULL, N'Prueba', 200000.0000, 0, 0, NULL, NULL)
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (95, N'Aforo # 1', 2000, 1, CAST(N'2018-05-27 12:20:00' AS SmallDateTime), NULL, NULL, 0, NULL, N'', 0.0000, 0, 0, NULL, NULL)
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (96, N'Localidad # 1', 144, 2, CAST(N'2018-05-27 12:20:00' AS SmallDateTime), NULL, NULL, 0, NULL, N'', 0.0000, 12, 12, N'Down-Up', N'R-L')
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (97, N'Prueba', 1476, 3, CAST(N'2018-05-27 12:20:00' AS SmallDateTime), NULL, NULL, 0, NULL, N'qwe', 121212.0000, 123, 12, NULL, NULL)
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (98, N'12323qwe', 276, 4, CAST(N'2018-05-27 12:20:00' AS SmallDateTime), 1, NULL, 1, NULL, N'', 0.0000, 23, 12, NULL, NULL)
GO
INSERT [dbo].[localidad] ([Id], [Nombre], [Aforo], [IdTipoLocalidad], [FechaCreacion], [TicketNumeroInicio], [TicketNumeroFinal], [RangoTickets], [IdTipoNumeracion], [Consumo], [ValorConsumo], [Filas], [Sillas], [OrientacionFilas], [OrientacionSillas]) VALUES (99, N'qwe21', 540, 3, CAST(N'2018-05-27 12:20:00' AS SmallDateTime), 12, NULL, 1, NULL, N'qweqwe', 123123.0000, 45, 12, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[localidad] OFF
GO
SET IDENTITY_INSERT [dbo].[LocalidadesVenueEvento] ON 

GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (20, 45, 89, 68)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (21, 45, 90, 68)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (22, 45, 91, 68)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (23, 45, 92, 68)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (24, 45, 93, 68)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (25, 46, 94, 69)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (26, 47, 95, 70)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (27, 47, 96, 70)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (28, 47, 97, 70)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (29, 47, 98, 70)
GO
INSERT [dbo].[LocalidadesVenueEvento] ([Id], [IdVenue], [IdLocalidad], [IdEvento]) VALUES (30, 47, 99, 70)
GO
SET IDENTITY_INSERT [dbo].[LocalidadesVenueEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[LogPrintTicket] ON 

GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (1, N'Se inicia el proceso', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (2, N'Proseso Iniciado', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (3, N'Impresion iniciada: Consultando la informacion de la transaccion a imprimir', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (4, N'Proseso Iniciado', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (5, N'Impresion iniciada: Consultando la informacion de la transaccion a imprimir', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (6, N'Proseso Iniciado', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (7, N'Impresion iniciada: Consultando la informacion de la transaccion a imprimir', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (8, N'Proseso Iniciado', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (9, N'Impresion iniciada: Consultando la informacion de la transaccion a imprimir', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (10, N'Proseso Iniciado', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (11, N'Impresion iniciada: Consultando la informacion de la transaccion a imprimir', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (12, N'se inicia la impresion del tiquete numero 288', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (13, N'se finaliza la impresion del tiquete numero 288', N'EXITOSO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (14, N'se inicia la impresion del tiquete numero 277', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (15, N'se finaliza la impresion del tiquete numero 277', N'EXITOSO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (16, N'se inicia la impresion del tiquete numero 266', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (17, N'se finaliza la impresion del tiquete numero 266', N'EXITOSO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (18, N'se inicia la impresion del tiquete numero 299', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (19, N'se finaliza la impresion del tiquete numero 299', N'EXITOSO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (20, N'se inicia la impresion del tiquete numero 287', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (21, N'se finaliza la impresion del tiquete numero 287', N'EXITOSO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (22, N'se inicia la impresion del tiquete numero 275', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (23, N'se finaliza la impresion del tiquete numero 275', N'EXITOSO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (24, N'se inicia la impresion del tiquete numero 263', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (25, N'se finaliza la impresion del tiquete numero 263', N'EXITOSO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (26, N'se inicia la impresion del tiquete numero 251', N'ENPROCESO', NULL)
GO
INSERT [dbo].[LogPrintTicket] ([id], [Descripcion], [Estado], [IdTransaccioTicketImpresion]) VALUES (27, N'se finaliza la impresion del tiquete numero 251', N'EXITOSO', NULL)
GO
SET IDENTITY_INSERT [dbo].[LogPrintTicket] OFF
GO
SET IDENTITY_INSERT [dbo].[paises] ON 

GO
INSERT [dbo].[paises] ([id], [nombre], [fechacreacion], [codigo]) VALUES (1, N'COLOMBIA', CAST(N'2016-08-11 07:53:52.120' AS DateTime), N'01')
GO
SET IDENTITY_INSERT [dbo].[paises] OFF
GO
SET IDENTITY_INSERT [dbo].[ticketsEtapa] ON 

GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (43, 89, 25, 6, 1500, 25000.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (44, 89, 26, 4, 1500, 200000.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (45, 92, 26, 5, 50, 1000000.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (46, 94, 27, NULL, 5000, 20000.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (47, 96, 28, NULL, 100, 22000.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (48, 97, 28, NULL, 500, 30000.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (49, 98, 29, NULL, 200, 50000.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (50, 96, 29, NULL, 44, 22222.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (51, 95, 29, NULL, 200, 23333.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (52, 97, 29, NULL, 1000, 444.0000)
GO
INSERT [dbo].[ticketsEtapa] ([id], [idLocalidad], [idEtapa], [IdTipoServicio], [Aforo], [Precio]) VALUES (53, 98, 30, NULL, 500, 1233344.0000)
GO
SET IDENTITY_INSERT [dbo].[ticketsEtapa] OFF
GO
SET IDENTITY_INSERT [dbo].[ticketsEvento] ON 

GO
INSERT [dbo].[ticketsEvento] ([id], [idEvento], [cantidad], [inicioNumeracion], [JsonCanvas], [finNumeracion], [ticketType], [ticketImage]) VALUES (19, 68, 5000, 0, N'<?xml version="1.0" encoding="UTF-8" standalone="no" ?>
<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">
<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="800" height="680" viewBox="0 0 800 680" xml:space="preserve">
<desc>Created with Fabric.js 1.7.14</desc>
<defs>
</defs>
<rect x="0" y="0" width="800" height="680" fill="#ddd"></rect>
<g transform="translate(119.5 336.5)">
<image xlink:href="data:image/png;base64,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" x="-119.5" y="-336.5" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="239" height="673" preserveAspectRatio="none"></image>
</g>
	<g id="Numero1" transform="translate(117.67 21.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero2" transform="translate(117.67 469.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero3" transform="translate(117.67 518.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero4" transform="translate(117.67 655.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(122.52 377.67)">
		<text font-family="Arial Black" font-size="18" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-57.02" y="5.67" fill="#000000">25,000.00 $</tspan>
		</text>
	</g>
	<g id="NumericBarcode" transform="translate(133.54 442.28)">
		<text font-family="Arial Black" font-size="12" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-72.04" y="3.78" fill="#000000">131717728910180157</tspan>
		</text>
	</g>
	<g id="Responsable1" transform="translate(11.4 243) rotate(90)">
		<text font-family="Arial Black" font-size="5" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-81.67" y="-1.7" fill="#000000">RESPONSABLE: TRIADA COLOMBIA S.A.S NIT: 900136882-9</tspan>
			<tspan x="-122.5" y="4.85" fill="#000000">TICKETEVENTO NO SE HACE RESPONSABLE DE COMERCIALIZACIÓN DE ESTA ENTRADA</tspan>
		</text>
	</g>
	<g id="Responsable2" transform="translate(135 636.6)">
		<text font-family="Arial Black" font-size="5" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-81.67" y="-1.7" fill="#000000">RESPONSABLE: TRIADA COLOMBIA S.A.S NIT: 900136882-9</tspan>
			<tspan x="-122.5" y="4.85" fill="#000000">TICKETEVENTO NO SE HACE RESPONSABLE DE COMERCIALIZACIÓN DE ESTA ENTRADA</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(126.33 341.59)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-89.83" y="-4.77" fill="#000000">Parque Norte, Medellín,</tspan>
			<tspan x="-89.83" y="13.59" fill="#000000">Antioquia, Colombia</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(93.49 56.67)">
		<text font-family="Arial Black" font-size="18" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-60.99" y="5.67" fill="#000000">BAUM PARK</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(117.12 253.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-62.62" y="4.41" fill="#000000">viernes 25 mayo</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(118.84 224.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-58.34" y="4.41" fill="#000000">Edad Minima18</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(117.34 290.59)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-54.84" y="-4.77" fill="#000000">Apertarura de</tspan>
			<tspan x="-54.84" y="13.59" fill="#000000">puertas: 20:48</tspan>
		</text>
	</g>
	<g id="LocalidadText" transform="translate(14.5 325.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-1" y="4.41" fill="#000000"></tspan>
		</text>
	</g>
<g transform="translate(114.35 409.47) scale(0.45 0.45)">
<image id="ImageBarcode" xlink:href="data:image/png;base64,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" x="-199" y="-30" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="398" height="60" preserveAspectRatio="none"></image>
</g>
<g transform="translate(125.75 141.87) scale(0.17 0.17)">
<image id="image" xlink:href="data:image/png;base64,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" x="-540" y="-270" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="1080" height="540" preserveAspectRatio="none"></image>
</g>
</svg>', 0, N'grande', NULL)
GO
INSERT [dbo].[ticketsEvento] ([id], [idEvento], [cantidad], [inicioNumeracion], [JsonCanvas], [finNumeracion], [ticketType], [ticketImage]) VALUES (20, 69, 5000, 0, N'<?xml version="1.0" encoding="UTF-8" standalone="no" ?>
<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">
<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="800" height="680" viewBox="0 0 800 680" xml:space="preserve">
<desc>Created with Fabric.js 1.7.14</desc>
<defs>
</defs>
<rect x="0" y="0" width="800" height="680" fill="#ddd"></rect>
<g transform="translate(119.5 336.5)">
<image xlink:href="data:image/png;base64,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" x="-119.5" y="-336.5" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="239" height="673" preserveAspectRatio="none"></image>
</g>
	<g id="Numero1" transform="translate(117.67 21.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero2" transform="translate(117.67 469.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero3" transform="translate(117.67 518.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero4" transform="translate(117.67 655.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(126.52 370.67)">
		<text font-family="Arial Black" font-size="18" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-57.02" y="5.67" fill="#000000">25,000.00 $</tspan>
		</text>
	</g>
	<g id="NumericBarcode" transform="translate(133.54 442.28)">
		<text font-family="Arial Black" font-size="12" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-72.04" y="3.78" fill="#000000">131717733506560326</tspan>
		</text>
	</g>
	<g id="Responsable1" transform="translate(11.4 243) rotate(90)">
		<text font-family="Arial Black" font-size="5" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-81.67" y="-1.7" fill="#000000">RESPONSABLE: TRIADA COLOMBIA S.A.S NIT: 900136882-9</tspan>
			<tspan x="-122.5" y="4.85" fill="#000000">TICKETEVENTO NO SE HACE RESPONSABLE DE COMERCIALIZACIÓN DE ESTA ENTRADA</tspan>
		</text>
	</g>
	<g id="Responsable2" transform="translate(135 636.6)">
		<text font-family="Arial Black" font-size="5" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-81.67" y="-1.7" fill="#000000">RESPONSABLE: TRIADA COLOMBIA S.A.S NIT: 900136882-9</tspan>
			<tspan x="-122.5" y="4.85" fill="#000000">TICKETEVENTO NO SE HACE RESPONSABLE DE COMERCIALIZACIÓN DE ESTA ENTRADA</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(123.68 565.59)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-113.18" y="-4.77" fill="#000000">Parque de Las Aguas,</tspan>
			<tspan x="-113.18" y="13.59" fill="#000000">Barbosa, Antioquia, Colombia</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(84.45 56.67)">
		<text font-family="Arial Black" font-size="18" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-51.95" y="5.67" fill="#000000">Evento # 1</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(87.12 309.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-62.62" y="4.41" fill="#000000">viernes 25 mayo</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(90.84 275.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-58.34" y="4.41" fill="#000000">Edad Minima18</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(141.56 330.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-110.06" y="4.41" fill="#000000">Apertarura de puertas: 20:55</tspan>
		</text>
	</g>
	<g id="LocalidadText" transform="translate(14.5 325.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-1" y="4.41" fill="#000000"></tspan>
		</text>
	</g>
<g transform="translate(114.35 409.47) scale(0.45 0.45)">
<image id="ImageBarcode" xlink:href="data:image/png;base64,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" x="-199" y="-30" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="398" height="60" preserveAspectRatio="none"></image>
</g>
</svg>', 0, N'grande', NULL)
GO
INSERT [dbo].[ticketsEvento] ([id], [idEvento], [cantidad], [inicioNumeracion], [JsonCanvas], [finNumeracion], [ticketType], [ticketImage]) VALUES (21, 70, 4996, 0, N'', 0, N'grande', NULL)
GO
INSERT [dbo].[ticketsEvento] ([id], [idEvento], [cantidad], [inicioNumeracion], [JsonCanvas], [finNumeracion], [ticketType], [ticketImage]) VALUES (22, 70, 4996, 0, N'<?xml version="1.0" encoding="UTF-8" standalone="no" ?>
<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">
<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="800" height="680" viewBox="0 0 800 680" xml:space="preserve">
<desc>Created with Fabric.js 1.7.14</desc>
<defs>
</defs>
<rect x="0" y="0" width="800" height="680" fill="#ddd"></rect>
<g transform="translate(119.5 336.5)">
<image xlink:href="data:image/png;base64,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" x="-119.5" y="-336.5" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="239" height="673" preserveAspectRatio="none"></image>
</g>
	<g id="Numero1" transform="translate(117.67 21.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero2" transform="translate(117.67 469.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero3" transform="translate(117.67 518.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero4" transform="translate(117.67 655.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(126.52 370.67)">
		<text font-family="Arial Black" font-size="18" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-57.02" y="5.67" fill="#000000">25,000.00 $</tspan>
		</text>
	</g>
	<g id="NumericBarcode" transform="translate(133.54 442.28)">
		<text font-family="Arial Black" font-size="12" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-72.04" y="3.78" fill="#000000">131738425490433832</tspan>
		</text>
	</g>
	<g id="Responsable1" transform="translate(11.4 243) rotate(90)">
		<text font-family="Arial Black" font-size="5" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-81.67" y="-1.7" fill="#000000">RESPONSABLE: TRIADA COLOMBIA S.A.S NIT: 900136882-9</tspan>
			<tspan x="-122.5" y="4.85" fill="#000000">TICKETEVENTO NO SE HACE RESPONSABLE DE COMERCIALIZACIÓN DE ESTA ENTRADA</tspan>
		</text>
	</g>
	<g id="Responsable2" transform="translate(135 636.6)">
		<text font-family="Arial Black" font-size="5" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-81.67" y="-1.7" fill="#000000">RESPONSABLE: TRIADA COLOMBIA S.A.S NIT: 900136882-9</tspan>
			<tspan x="-122.5" y="4.85" fill="#000000">TICKETEVENTO NO SE HACE RESPONSABLE DE COMERCIALIZACIÓN DE ESTA ENTRADA</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(257.63 347.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-197.13" y="4.41" fill="#000000">Carrera 50B #123-25, Medellín, Antioquia, Colombia</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(84.45 56.67)">
		<text font-family="Arial Black" font-size="18" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-51.95" y="5.67" fill="#000000">Evento # 1</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(77.79 309.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-53.29" y="4.41" fill="#000000">lunes 18 junio</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(90.84 275.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-58.34" y="4.41" fill="#000000">Edad Minima18</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(264.56 309.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-110.06" y="4.41" fill="#000000">Apertarura de puertas: 19:42</tspan>
		</text>
	</g>
	<g id="LocalidadText" transform="translate(14.5 325.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-1" y="4.41" fill="#000000"></tspan>
		</text>
	</g>
<g transform="translate(114.35 409.47) scale(0.45 0.45)">
<image id="ImageBarcode" xlink:href="data:image/png;base64,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" x="-199" y="-30" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="398" height="60" preserveAspectRatio="none"></image>
</g>
</svg>', 0, N'grande', NULL)
GO
INSERT [dbo].[ticketsEvento] ([id], [idEvento], [cantidad], [inicioNumeracion], [JsonCanvas], [finNumeracion], [ticketType], [ticketImage]) VALUES (23, 70, 4996, 0, N'<?xml version="1.0" encoding="UTF-8" standalone="no" ?>
<!DOCTYPE svg PUBLIC "-//W3C//DTD SVG 1.1//EN" "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd">
<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="800" height="680" viewBox="0 0 800 680" xml:space="preserve">
<desc>Created with Fabric.js 1.7.14</desc>
<defs>
</defs>
<rect x="0" y="0" width="800" height="680" fill="#ddd"></rect>
<g transform="translate(119.5 336.5)">
<image xlink:href="data:image/png;base64,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" x="-119.5" y="-336.5" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="239" height="673" preserveAspectRatio="none"></image>
</g>
	<g id="Numero1" transform="translate(117.67 21.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero2" transform="translate(117.67 469.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero3" transform="translate(117.67 518.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Numero4" transform="translate(117.67 655.15)">
		<text font-family="Arial Black" font-size="10" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-29.17" y="3.15" fill="#000000">TICKET 99</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(126.52 370.67)">
		<text font-family="Arial Black" font-size="18" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-57.02" y="5.67" fill="#000000">25,000.00 $</tspan>
		</text>
	</g>
	<g id="NumericBarcode" transform="translate(133.54 442.28)">
		<text font-family="Arial Black" font-size="12" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-72.04" y="3.78" fill="#000000">131738429355261796</tspan>
		</text>
	</g>
	<g id="Responsable1" transform="translate(11.4 243) rotate(90)">
		<text font-family="Arial Black" font-size="5" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-81.67" y="-1.7" fill="#000000">RESPONSABLE: TRIADA COLOMBIA S.A.S NIT: 900136882-9</tspan>
			<tspan x="-122.5" y="4.85" fill="#000000">TICKETEVENTO NO SE HACE RESPONSABLE DE COMERCIALIZACIÓN DE ESTA ENTRADA</tspan>
		</text>
	</g>
	<g id="Responsable2" transform="translate(135 636.6)">
		<text font-family="Arial Black" font-size="5" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-81.67" y="-1.7" fill="#000000">RESPONSABLE: TRIADA COLOMBIA S.A.S NIT: 900136882-9</tspan>
			<tspan x="-122.5" y="4.85" fill="#000000">TICKETEVENTO NO SE HACE RESPONSABLE DE COMERCIALIZACIÓN DE ESTA ENTRADA</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(257.63 347.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-197.13" y="4.41" fill="#000000">Carrera 50B #123-25, Medellín, Antioquia, Colombia</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(84.45 56.67)">
		<text font-family="Arial Black" font-size="18" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-51.95" y="5.67" fill="#000000">Evento # 1</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(77.79 309.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-53.29" y="4.41" fill="#000000">lunes 18 junio</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(90.84 275.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-58.34" y="4.41" fill="#000000">Edad Minima18</tspan>
		</text>
	</g>
	<g id="Element" transform="translate(264.56 309.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-110.06" y="4.41" fill="#000000">Apertarura de puertas: 19:48</tspan>
		</text>
	</g>
	<g id="LocalidadText" transform="translate(14.5 325.41)">
		<text font-family="Arial Black" font-size="14" font-weight="normal" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" >
			<tspan x="-1" y="4.41" fill="#000000"></tspan>
		</text>
	</g>
<g transform="translate(114.35 409.47) scale(0.45 0.45)">
<image id="ImageBarcode" xlink:href="data:image/png;base64,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" x="-199" y="-30" style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(0,0,0); fill-rule: nonzero; opacity: 1;" width="398" height="60" preserveAspectRatio="none"></image>
</g>
</svg>', 0, N'grande', NULL)
GO
SET IDENTITY_INSERT [dbo].[ticketsEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[tipodocumento] ON 

GO
INSERT [dbo].[tipodocumento] ([id], [nombre], [codigo], [fechacreacion]) VALUES (1, N'Cedula', N'CC', CAST(N'2016-08-22 07:57:03.513' AS DateTime))
GO
INSERT [dbo].[tipodocumento] ([id], [nombre], [codigo], [fechacreacion]) VALUES (2, N'Nit', N'NIT', CAST(N'2016-08-22 07:57:03.530' AS DateTime))
GO
INSERT [dbo].[tipodocumento] ([id], [nombre], [codigo], [fechacreacion]) VALUES (3, N'Tarjeta Identidad', N'TI', CAST(N'2016-08-22 07:57:03.530' AS DateTime))
GO
INSERT [dbo].[tipodocumento] ([id], [nombre], [codigo], [fechacreacion]) VALUES (4, N'extranjero', N'2222', CAST(N'2016-12-14 08:57:13.863' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tipodocumento] OFF
GO
SET IDENTITY_INSERT [dbo].[tipoempresa] ON 

GO
INSERT [dbo].[tipoempresa] ([id], [nombre], [codigo], [fechacreacion]) VALUES (1, N'Eventos', N'Eventos', CAST(N'2016-08-20 07:14:37.383' AS DateTime))
GO
INSERT [dbo].[tipoempresa] ([id], [nombre], [codigo], [fechacreacion]) VALUES (2, N'Control', N'Control', CAST(N'2016-08-20 07:14:37.440' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tipoempresa] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoEvento] ON 

GO
INSERT [dbo].[TipoEvento] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (1, N'TE1', N'DES', CAST(N'2016-08-24 10:49:52.673' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TipoEvento] OFF
GO
SET IDENTITY_INSERT [dbo].[tipoLocalidad] ON 

GO
INSERT [dbo].[tipoLocalidad] ([id], [nombre], [descripcion], [fechaCreacion]) VALUES (1, N'Aforo', N'Aforo', CAST(N'2017-01-30 22:55:00' AS SmallDateTime))
GO
INSERT [dbo].[tipoLocalidad] ([id], [nombre], [descripcion], [fechaCreacion]) VALUES (2, N'Filas y Sillas', N'Filas y Columnas', CAST(N'2017-01-30 22:55:00' AS SmallDateTime))
GO
INSERT [dbo].[tipoLocalidad] ([id], [nombre], [descripcion], [fechaCreacion]) VALUES (3, N'Mesas y Sillas', N'Mesas y Sillas', CAST(N'2017-01-30 22:55:00' AS SmallDateTime))
GO
INSERT [dbo].[tipoLocalidad] ([id], [nombre], [descripcion], [fechaCreacion]) VALUES (4, N'Palco', N'Palco', CAST(N'2017-01-30 22:55:00' AS SmallDateTime))
GO
SET IDENTITY_INSERT [dbo].[tipoLocalidad] OFF
GO
SET IDENTITY_INSERT [dbo].[tipomoneda] ON 

GO
INSERT [dbo].[tipomoneda] ([id], [nombre], [codigo], [simbolo], [decimales], [fechacreacion]) VALUES (1, N'Peso Colombiano', N'COP', N'$', 3, CAST(N'2016-08-23 16:26:51.147' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tipomoneda] OFF
GO
SET IDENTITY_INSERT [dbo].[tipoNumeracion] ON 

GO
INSERT [dbo].[tipoNumeracion] ([Id], [Numeracion]) VALUES (1, N'A-Z, A-Z')
GO
INSERT [dbo].[tipoNumeracion] ([Id], [Numeracion]) VALUES (2, N'0-1, A-Z')
GO
INSERT [dbo].[tipoNumeracion] ([Id], [Numeracion]) VALUES (3, N'A-Z, 0-1')
GO
INSERT [dbo].[tipoNumeracion] ([Id], [Numeracion]) VALUES (4, N'0-1, 0-1')
GO
SET IDENTITY_INSERT [dbo].[tipoNumeracion] OFF
GO
SET IDENTITY_INSERT [dbo].[tipopago] ON 

GO
INSERT [dbo].[tipopago] ([id], [nombre], [fechacreacion]) VALUES (1, N'Efectivo', CAST(N'2016-08-11 08:21:28.110' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tipopago] OFF
GO
SET IDENTITY_INSERT [dbo].[tipopersona] ON 

GO
INSERT [dbo].[tipopersona] ([id], [nombre], [fechacreacion]) VALUES (1, N'Natural', CAST(N'2016-08-22 07:58:09.697' AS DateTime))
GO
INSERT [dbo].[tipopersona] ([id], [nombre], [fechacreacion]) VALUES (2, N'Juridica', CAST(N'2016-08-22 07:58:09.723' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tipopersona] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoRecaudo] ON 

GO
INSERT [dbo].[TipoRecaudo] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (1, N'Recaudo 1', N'Recaudo 1', CAST(N'2017-02-01 23:29:43.647' AS DateTime))
GO
INSERT [dbo].[TipoRecaudo] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (2, N'Recaudo 2', N'Recaudo 2', CAST(N'2017-02-01 23:29:43.647' AS DateTime))
GO
INSERT [dbo].[TipoRecaudo] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (3, N'Recaudo 3', N'Recaudo 3', CAST(N'2017-02-01 23:29:43.650' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TipoRecaudo] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoServicio] ON 

GO
INSERT [dbo].[TipoServicio] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (1, N'T1', N'D1', CAST(N'2016-08-24 10:49:24.003' AS DateTime))
GO
INSERT [dbo].[TipoServicio] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (2, N'Servicio 1', N'Servicio 1', CAST(N'2017-01-29 16:16:14.780' AS DateTime))
GO
INSERT [dbo].[TipoServicio] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (3, N'Servicio 2', N'Servicio 2', CAST(N'2017-01-29 16:16:14.783' AS DateTime))
GO
INSERT [dbo].[TipoServicio] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (4, N'Servicio 3', N'Servicio 3', CAST(N'2017-01-29 16:16:14.783' AS DateTime))
GO
INSERT [dbo].[TipoServicio] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (5, N'Servicio 4', N'Servicio 4', CAST(N'2017-01-29 16:16:14.783' AS DateTime))
GO
INSERT [dbo].[TipoServicio] ([id], [nombre], [descripcion], [fechacreacion]) VALUES (6, N'Servicio 5', N'Servicio 5', CAST(N'2017-01-29 16:16:14.783' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TipoServicio] OFF
GO
SET IDENTITY_INSERT [dbo].[tipoUsuario] ON 

GO
INSERT [dbo].[tipoUsuario] ([id], [code], [description], [createDate]) VALUES (2, N'001', N'Tipo 1', CAST(N'2016-12-27 12:21:00' AS SmallDateTime))
GO
INSERT [dbo].[tipoUsuario] ([id], [code], [description], [createDate]) VALUES (3, N'002', N'Tipo 2', CAST(N'2016-12-27 12:21:00' AS SmallDateTime))
GO
INSERT [dbo].[tipoUsuario] ([id], [code], [description], [createDate]) VALUES (4, N'003', N'Tipo 3', CAST(N'2016-12-27 12:21:00' AS SmallDateTime))
GO
SET IDENTITY_INSERT [dbo].[tipoUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 

GO
INSERT [dbo].[usuarios] ([id], [nombre], [contrasena], [bloqueado], [email], [nombres], [apellidos], [direccion], [telefono], [fecha], [fechacreacion], [tipoUsuario_id]) VALUES (1, N'admin', N'123', 0, N'admin@gmail.com', N'David', N'Vivas', N'ND', N'XXXX', NULL, CAST(N'2016-08-10 13:06:33.213' AS DateTime), NULL)
GO
INSERT [dbo].[usuarios] ([id], [nombre], [contrasena], [bloqueado], [email], [nombres], [apellidos], [direccion], [telefono], [fecha], [fechacreacion], [tipoUsuario_id]) VALUES (2, N'Usuario', N'123', 0, N'correo@correo.com', N'Mario', N'Bendetti', N'Cra 23', N'12332112', CAST(N'2000-12-06 00:00:00.000' AS DateTime), CAST(N'2016-12-27 12:26:23.543' AS DateTime), 3)
GO
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
SET IDENTITY_INSERT [dbo].[UVT] ON 

GO
INSERT [dbo].[UVT] ([id], [anio], [valor], [fechacreacion]) VALUES (1, 2000, CAST(12000.000 AS Decimal(18, 3)), CAST(N'2016-08-24 10:51:16.067' AS DateTime))
GO
INSERT [dbo].[UVT] ([id], [anio], [valor], [fechacreacion]) VALUES (2, 3, CAST(1.000 AS Decimal(18, 3)), CAST(N'2016-12-14 08:49:55.570' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[UVT] OFF
GO
SET IDENTITY_INSERT [dbo].[venue] ON 

GO
INSERT [dbo].[venue] ([id], [nombre], [direccion], [ciudad_id], [telefono], [email], [foto], [aforo], [fechaCreacion], [Latitud], [Longitud], [CityName], [DepartametName], [CountryName], [PostalCode], [GeoCode]) VALUES (45, N'Parque Norte', N'Parque Norte, Medellín, Antioquia, Colombia', NULL, N'12321312', N'caaluna@gmai.com', NULL, 0, NULL, N'6.272664599999999', N'-75.56878919999997', N'', N'Antioquia', N'Colombia', N'050010', N'ChIJ1wnwINwoRI4RsrkY66o1MOo')
GO
INSERT [dbo].[venue] ([id], [nombre], [direccion], [ciudad_id], [telefono], [email], [foto], [aforo], [fechaCreacion], [Latitud], [Longitud], [CityName], [DepartametName], [CountryName], [PostalCode], [GeoCode]) VALUES (46, N'Paque de las aguas', N'Parque de Las Aguas, Barbosa, Antioquia, Colombia', NULL, N'232324', N'caaluna@gmai.com', NULL, 0, NULL, N'6.406612099999999', N'-75.4226658', N'', N'Antioquia', N'Colombia', N'051027', N'ChIJozbe1_c8RI4RJpk5rwitF8I')
GO
INSERT [dbo].[venue] ([id], [nombre], [direccion], [ciudad_id], [telefono], [email], [foto], [aforo], [fechaCreacion], [Latitud], [Longitud], [CityName], [DepartametName], [CountryName], [PostalCode], [GeoCode]) VALUES (47, N'Parque Norte', N'Carrera 50B #123-25, Medellín, Antioquia, Colombia', 81, N'123123', N'caaluna@gmai.com', NULL, 0, NULL, N'6.306419500000001', N'-75.55268619999998', N'Medellín', N'Antioquia', N'Colombia', N'050001', N'ChIJo52SEj8vRI4REPirq1QFG7o')
GO
SET IDENTITY_INSERT [dbo].[venue] OFF
GO
ALTER TABLE [dbo].[permisos] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[permisos_rol] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[roles] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[roles_usuario] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[usuarios_portal] ADD  DEFAULT (getdate()) FOR [fechacreacion]
GO
ALTER TABLE [dbo].[canalesVentaEvento]  WITH CHECK ADD FOREIGN KEY([idcanal])
REFERENCES [dbo].[CanalesVenta] ([id])
GO
ALTER TABLE [dbo].[canalesVentaEvento]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[categoriasEvento]  WITH CHECK ADD FOREIGN KEY([idEvento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[categoriasEvento]  WITH CHECK ADD  CONSTRAINT [FK_categoriasEvento_categorias] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[categorias] ([id])
GO
ALTER TABLE [dbo].[categoriasEvento] CHECK CONSTRAINT [FK_categoriasEvento_categorias]
GO
ALTER TABLE [dbo].[ciudades]  WITH CHECK ADD  CONSTRAINT [FK_Ciudades_Departamentos] FOREIGN KEY([iddepartamento])
REFERENCES [dbo].[departamentos] ([id])
GO
ALTER TABLE [dbo].[ciudades] CHECK CONSTRAINT [FK_Ciudades_Departamentos]
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD  CONSTRAINT [FK__clientes__genero__06CD04F7] FOREIGN KEY([genero_id])
REFERENCES [dbo].[Genero] ([id])
GO
ALTER TABLE [dbo].[clientes] CHECK CONSTRAINT [FK__clientes__genero__06CD04F7]
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD  CONSTRAINT [FK__clientes__tipoDo__07C12930] FOREIGN KEY([tipoDocumento_id])
REFERENCES [dbo].[tipodocumento] ([id])
GO
ALTER TABLE [dbo].[clientes] CHECK CONSTRAINT [FK__clientes__tipoDo__07C12930]
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD  CONSTRAINT [FK__clientes__tipoPe__08B54D69] FOREIGN KEY([tipoPersona_id])
REFERENCES [dbo].[tipopersona] ([id])
GO
ALTER TABLE [dbo].[clientes] CHECK CONSTRAINT [FK__clientes__tipoPe__08B54D69]
GO
ALTER TABLE [dbo].[departamentos]  WITH CHECK ADD  CONSTRAINT [FK_Departamentos_Paises] FOREIGN KEY([idpais])
REFERENCES [dbo].[paises] ([id])
GO
ALTER TABLE [dbo].[departamentos] CHECK CONSTRAINT [FK_Departamentos_Paises]
GO
ALTER TABLE [dbo].[direccionesClientes]  WITH CHECK ADD  CONSTRAINT [FK__direccion__ciuda__0A9D95DB] FOREIGN KEY([ciudad_id])
REFERENCES [dbo].[ciudades] ([id])
GO
ALTER TABLE [dbo].[direccionesClientes] CHECK CONSTRAINT [FK__direccion__ciuda__0A9D95DB]
GO
ALTER TABLE [dbo].[direccionesClientes]  WITH CHECK ADD  CONSTRAINT [FK__direccion__clien__0B91BA14] FOREIGN KEY([cliente_id])
REFERENCES [dbo].[clientes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[direccionesClientes] CHECK CONSTRAINT [FK__direccion__clien__0B91BA14]
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([ciudad_idRL])
REFERENCES [dbo].[ciudades] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([ciudad_id])
REFERENCES [dbo].[ciudades] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([sexo_idRL])
REFERENCES [dbo].[Genero] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([tipoDocumentoRL_id])
REFERENCES [dbo].[tipodocumento] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([tipoDocumento_id])
REFERENCES [dbo].[tipodocumento] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([tipoEmpresa_id])
REFERENCES [dbo].[tipoempresa] ([id])
GO
ALTER TABLE [dbo].[empresarios]  WITH CHECK ADD FOREIGN KEY([tipoPersona_id])
REFERENCES [dbo].[tipopersona] ([id])
GO
ALTER TABLE [dbo].[etapasVentaEvento]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[eventos]  WITH CHECK ADD FOREIGN KEY([empresario_id])
REFERENCES [dbo].[empresarios] ([id])
GO
ALTER TABLE [dbo].[eventos]  WITH CHECK ADD FOREIGN KEY([tipoEvento])
REFERENCES [dbo].[TipoEvento] ([id])
GO
ALTER TABLE [dbo].[eventos]  WITH CHECK ADD FOREIGN KEY([tipoRecaudo_id])
REFERENCES [dbo].[TipoRecaudo] ([id])
GO
ALTER TABLE [dbo].[funcionesEventos]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[impuestosEvento]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[impuestosEvento]  WITH CHECK ADD FOREIGN KEY([idimpuesto])
REFERENCES [dbo].[Impuesto] ([id])
GO
ALTER TABLE [dbo].[localidad]  WITH CHECK ADD FOREIGN KEY([IdTipoNumeracion])
REFERENCES [dbo].[tipoNumeracion] ([Id])
GO
ALTER TABLE [dbo].[localidad]  WITH CHECK ADD FOREIGN KEY([IdTipoLocalidad])
REFERENCES [dbo].[tipoLocalidad] ([id])
GO
ALTER TABLE [dbo].[LocalidadesVenueEvento]  WITH CHECK ADD FOREIGN KEY([IdEvento])
REFERENCES [dbo].[eventos] ([id])
GO
ALTER TABLE [dbo].[LocalidadesVenueEvento]  WITH CHECK ADD FOREIGN KEY([IdLocalidad])
REFERENCES [dbo].[localidad] ([Id])
GO
ALTER TABLE [dbo].[LocalidadesVenueEvento]  WITH CHECK ADD FOREIGN KEY([IdVenue])
REFERENCES [dbo].[venue] ([id])
GO
ALTER TABLE [dbo].[LogPrintTicket]  WITH CHECK ADD FOREIGN KEY([IdTransaccioTicketImpresion])
REFERENCES [dbo].[transaccionTicketsImpresion] ([id])
GO
ALTER TABLE [dbo].[mapasVenue]  WITH CHECK ADD FOREIGN KEY([idMapa])
REFERENCES [dbo].[mapas] ([id])
GO
ALTER TABLE [dbo].[mapasVenue]  WITH CHECK ADD FOREIGN KEY([idVenue])
REFERENCES [dbo].[venue] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[permisos_rol]  WITH CHECK ADD  CONSTRAINT [fk_permisos_rol_permisos] FOREIGN KEY([idpermiso])
REFERENCES [dbo].[permisos] ([id])
GO
ALTER TABLE [dbo].[permisos_rol] CHECK CONSTRAINT [fk_permisos_rol_permisos]
GO
ALTER TABLE [dbo].[permisos_rol]  WITH CHECK ADD  CONSTRAINT [fk_permisos_rol_roles] FOREIGN KEY([idrol])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[permisos_rol] CHECK CONSTRAINT [fk_permisos_rol_roles]
GO
ALTER TABLE [dbo].[roles_usuario]  WITH CHECK ADD  CONSTRAINT [fk_roles_usuario_roles] FOREIGN KEY([idrol])
REFERENCES [dbo].[roles] ([id])
GO
ALTER TABLE [dbo].[roles_usuario] CHECK CONSTRAINT [fk_roles_usuario_roles]
GO
ALTER TABLE [dbo].[roles_usuario]  WITH CHECK ADD  CONSTRAINT [fk_roles_usuario_usuarios] FOREIGN KEY([idusuario])
REFERENCES [dbo].[usuarios] ([id])
GO
ALTER TABLE [dbo].[roles_usuario] CHECK CONSTRAINT [fk_roles_usuario_usuarios]
GO
ALTER TABLE [dbo].[serviciosEvento]  WITH CHECK ADD FOREIGN KEY([idevento])
REFERENCES [dbo].[eventos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[serviciosEvento]  WITH CHECK ADD FOREIGN KEY([idservicio])
REFERENCES [dbo].[TipoServicio] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD FOREIGN KEY([idEstado])
REFERENCES [dbo].[EstadoImpresion] ([id])
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD  CONSTRAINT [FK_tickets_ticketsEtapa] FOREIGN KEY([idTicketsEtapa])
REFERENCES [dbo].[ticketsEtapa] ([id])
GO
ALTER TABLE [dbo].[tickets] CHECK CONSTRAINT [FK_tickets_ticketsEtapa]
GO
ALTER TABLE [dbo].[tickets]  WITH CHECK ADD  CONSTRAINT [FK_tickets_ticketsEvento] FOREIGN KEY([idTicketsEvento])
REFERENCES [dbo].[ticketsEvento] ([id])
GO
ALTER TABLE [dbo].[tickets] CHECK CONSTRAINT [FK_tickets_ticketsEvento]
GO
ALTER TABLE [dbo].[ticketsEtapa]  WITH CHECK ADD FOREIGN KEY([IdTipoServicio])
REFERENCES [dbo].[TipoServicio] ([id])
GO
ALTER TABLE [dbo].[ticketsEtapa]  WITH CHECK ADD  CONSTRAINT [FK_ticketsEtapa_etapasVentaEvento] FOREIGN KEY([idEtapa])
REFERENCES [dbo].[etapasVentaEvento] ([id])
GO
ALTER TABLE [dbo].[ticketsEtapa] CHECK CONSTRAINT [FK_ticketsEtapa_etapasVentaEvento]
GO
ALTER TABLE [dbo].[ticketsEtapa]  WITH CHECK ADD  CONSTRAINT [FK_ticketsEtapa_localidad] FOREIGN KEY([idLocalidad])
REFERENCES [dbo].[localidad] ([Id])
GO
ALTER TABLE [dbo].[ticketsEtapa] CHECK CONSTRAINT [FK_ticketsEtapa_localidad]
GO
ALTER TABLE [dbo].[ticketsEvento]  WITH CHECK ADD FOREIGN KEY([idEvento])
REFERENCES [dbo].[eventos] ([id])
GO
ALTER TABLE [dbo].[transaccionPullTicketsImpresion]  WITH CHECK ADD FOREIGN KEY([idTicket])
REFERENCES [dbo].[EstadoImpresion] ([id])
GO
ALTER TABLE [dbo].[transaccionTicketsImpresion]  WITH CHECK ADD FOREIGN KEY([idEstado])
REFERENCES [dbo].[EstadoImpresion] ([id])
GO
ALTER TABLE [dbo].[transaccionTicketsImpresion]  WITH CHECK ADD FOREIGN KEY([idTicket])
REFERENCES [dbo].[tickets] ([id])
GO
ALTER TABLE [dbo].[transaccionTicketsImpresion]  WITH CHECK ADD  CONSTRAINT [FK_transaccionTicketsImpresion_ticketsEvento] FOREIGN KEY([idTicketEvento])
REFERENCES [dbo].[ticketsEvento] ([id])
GO
ALTER TABLE [dbo].[transaccionTicketsImpresion] CHECK CONSTRAINT [FK_transaccionTicketsImpresion_ticketsEvento]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD FOREIGN KEY([tipoUsuario_id])
REFERENCES [dbo].[tipoUsuario] ([id])
GO
ALTER TABLE [dbo].[venue]  WITH CHECK ADD FOREIGN KEY([ciudad_id])
REFERENCES [dbo].[ciudades] ([id])
GO
/****** Object:  StoredProcedure [dbo].[sp_ResetLocalidad]    Script Date: 16/07/2018 15:29:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_ResetLocalidad](
	@LocalidadID int
) as begin
	delete from sillas where id in (select idSilla from mesasFilasSillas where idMesasFilas in (select id from mesasFilas where id in (select idMesasFilas from localidadMesasFilasSillas where idLocalidad = @LocalidadID)))
	delete from mesasFilasSillas where idMesasFilas in (select id from mesasFilas where id in (select idMesasFilas from localidadMesasFilasSillas where idLocalidad = @LocalidadID))
	delete from mesasFilas where id in (select idMesasFilas from localidadMesasFilasSillas where idLocalidad = @LocalidadID)
	delete from localidadMesasFilasSillas where idLocalidad = @LocalidadID
end

GO
