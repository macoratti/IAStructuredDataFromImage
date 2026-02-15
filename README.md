# 📄 Receipt Vision Extractor (.NET + Ollama)

Projeto de exemplo demonstrando como extrair **dados estruturados de imagens de recibos** usando:

- 🧠 Modelo multimodal `llama3.2-vision`
- 🖥️ Ollama rodando localmente
- ⚙️ .NET 8/9
- 📦 Microsoft.Extensions.AI
- ✅ Validação determinística pós-processamento

O objetivo é transformar uma imagem de recibo em **JSON estruturado tipado**, com validação matemática e controle de consistência.

---

## 🚀 O que este projeto demonstra

- Execução de modelo multimodal local com Ollama
- Envio de imagem via `DataContent`
- Uso de `IChatClient`
- Saída fortemente tipada com `GetResponseAsync<T>`
- Geração automática de JSON Schema
- Validação determinística dos dados extraídos
- Arquitetura organizada por responsabilidade

---

## 🧠 Como funciona

Fluxo completo:
Imagem do recibo
->
Modelo llama3.2-vision
->
JSON estruturado
->
Desserialização automática para objeto C#
->
Validação matemática e estrutural
->
Exibição no console

## 🧩 Principais Componentes

### 🔹 ReceiptExtractionService
Orquestra a comunicação com o modelo e retorna objeto tipado.

### 🔹 PromptBuilder
Define regras para evitar alucinações e forçar JSON estruturado.

### 🔹 ReceiptValidator
Valida:
- Campos obrigatórios
- Soma dos itens = Subtotal
- Subtotal + Tax = Total
- Valores negativos

### 🔹 OllamaConfiguration
Configura o `HttpClient` e registra o modelo no container DI.

---

## 📦 Pré-requisitos

- .NET 8 ou 9
- Ollama instalado
- Modelo multimodal baixado

---

## 🔧 Instalação

### 1️⃣ Instale o Ollama

https://ollama.com

### 2️⃣ Baixe o modelo

```bash
ollama pull llama3.2-vision
