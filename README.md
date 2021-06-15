## Resumo

Este Asset permite instantanciar pontos flutuantes definindo um texto e uma cor como parâmetro

Este pacote contém:

- `FPText`: Prefab com um Text
- `FPCanvas`: Prefab com um Canvas

### FloatPointBehaviour

Este behaviour é responsável por instanciar um Canvas e Text, além de customizar as mensagens 

**Propriedades**
| Membro   |      Descrição      |
|----------|-------------|
| `Settings` | Variável que armazena o prefab `FPText` e `FPCanvas` neste behaviour |

**Métodos**
| Membro   |      Descrição      |
|----------|-------------|
| `Show` | Sobreescreve o conteúdo de um Text possibilitando passar uma mensagem de texto e uma cor como parametro e instancia um prefab `FPText` dentro de um Canvas também instanciado |