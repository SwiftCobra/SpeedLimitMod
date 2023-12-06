import * as esbuild from 'esbuild'

let ctx = await esbuild.context({
  entryPoints: ['src/dev-env.jsx'],
  bundle: true,
  outfile: 'dev-env/dist/dev-bundle.js',
})

let { host, port } = await ctx.serve({
  servedir: 'dev-env',
})