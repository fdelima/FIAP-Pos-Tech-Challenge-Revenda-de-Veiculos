/****** Object:  Table [dbo].[veiculo]    Script Date: 22/04/2025 16:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[veiculo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[veiculo](
	[id_veiculo] [uniqueidentifier] NOT NULL,
	[marca] [nvarchar](100) NOT NULL,
	[modelo] [nvarchar](100) NOT NULL,
	[ano_fabricacao] [int] NOT NULL,
	[ano_modelo] [int] NOT NULL,
	[placa] [nchar](7) NOT NULL,
	[renavam] [nchar](11) NOT NULL,
	[preco] [decimal](18, 2) NOT NULL,
	[status] [nchar](10) NOT NULL,
 CONSTRAINT [PK_veiculo] PRIMARY KEY CLUSTERED 
(
	[id_veiculo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[veiculo_foto]    Script Date: 22/04/2025 16:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[veiculo_foto]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[veiculo_foto](
	[id_veiculo_foto] [uniqueidentifier] NOT NULL,
	[id_veiculo] [uniqueidentifier] NOT NULL,
	[imagem_base64] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_veiculo_foto] PRIMARY KEY CLUSTERED 
(
	[id_veiculo_foto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Table [dbo].[veiculo_pagamento]    Script Date: 22/04/2025 16:54:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[veiculo_pagamento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[veiculo_pagamento](
	[id_veiculo_pagamento] [uniqueidentifier] NOT NULL,
	[id_veiculo] [uniqueidentifier] NOT NULL,
	[data] [datetime] NOT NULL,
	[valor_recebido] [decimal](18, 2) NOT NULL,
	[banco] [nvarchar](100) NOT NULL,
	[conta] [nvarchar](100) NOT NULL,
	[cpf_cnpj] [nchar](14) NOT NULL,
 CONSTRAINT [PK_veiculo_pagamento] PRIMARY KEY CLUSTERED 
(
	[id_veiculo_pagamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO

/****** Object:  Index [IX_veiculo]    Script Date: 22/04/2025 16:54:47 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[veiculo]') AND name = N'IX_veiculo')
CREATE UNIQUE NONCLUSTERED INDEX [IX_veiculo] ON [dbo].[veiculo]
(
	[renavam] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_veiculo_foto_veiculo]') AND parent_object_id = OBJECT_ID(N'[dbo].[veiculo_foto]'))
ALTER TABLE [dbo].[veiculo_foto]  WITH CHECK ADD  CONSTRAINT [FK_veiculo_foto_veiculo] FOREIGN KEY([id_veiculo])
REFERENCES [dbo].[veiculo] ([id_veiculo])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_veiculo_foto_veiculo]') AND parent_object_id = OBJECT_ID(N'[dbo].[veiculo_foto]'))
ALTER TABLE [dbo].[veiculo_foto] CHECK CONSTRAINT [FK_veiculo_foto_veiculo]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_veiculo_pagamento_veiculo]') AND parent_object_id = OBJECT_ID(N'[dbo].[veiculo_pagamento]'))
ALTER TABLE [dbo].[veiculo_pagamento]  WITH CHECK ADD  CONSTRAINT [FK_veiculo_pagamento_veiculo] FOREIGN KEY([id_veiculo])
REFERENCES [dbo].[veiculo] ([id_veiculo])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_veiculo_pagamento_veiculo]') AND parent_object_id = OBJECT_ID(N'[dbo].[veiculo_pagamento]'))
ALTER TABLE [dbo].[veiculo_pagamento] CHECK CONSTRAINT [FK_veiculo_pagamento_veiculo]
GO