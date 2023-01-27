import fields from "@/components/fields/index"
import commons from "@/components/commons/index"

export default {
    install: function (vue) {
        vue.use(fields)
        vue.use(commons)
    }
}